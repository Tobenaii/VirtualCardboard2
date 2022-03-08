using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Modsys/Entity")]
public class ModEntity : ScriptableObject, ISerializationCallbackReceiver
{
    [PropertyOrder(10000)]
    [SerializeField] private EntityAuthoring _authoring;

    public Entity Instantiate(string name)
    {
        return _authoring.Instantiate(name);
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        _authoring.Convert(entity, dstManager, conversionSystem);
    }

    public Entity GetPrefab(EntityManager manager, string name)
    {
        var ecb = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>().CreateCommandBuffer();
        return _authoring.GetPrefab(manager, name);
    }

    [ShowIf("@UnityEngine.Application.isPlaying")] [PropertyOrder(100000)]
    [Button("Test")]
    private void DebugInstantiate()
    {
        Instantiate(this.name);
    }

    private void Register()
    {
        if (_authoring != null)
            _authoring.Archetype?.Register(this);
    }

    private void OnValidate()
    {
        Register();
        ValidateComponents();
    }

    public void ValidateComponents()
    {
        if (_authoring.Archetype != null)
            _authoring.ValidateComponents();
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        Register();
    }

    public void OnAfterDeserialize()
    {
    }
}

[System.Serializable]
[InlineProperty]
[HideLabel]
[HideReferenceObjectPicker]
public class EntityAuthoring
{
    [ShowIf("@_archetype != null")]
    [PropertyOrder(-1000)]
    [HideLabel]
    [ShowInInspector] public EntityTemplate Archetype => _archetype;
    [ShowIf("@_archetype == null")]
    [SerializeField] private EntityTemplate _archetype;
    [SerializeField]
    [ListDrawerSettings(IsReadOnly = true, Expanded = true, ShowItemCount = false)]
    [HideReferenceObjectPicker]
    [ShowIf("@_archetype != null")]
    [LabelText("@NiceArchetypeName")]
    private List<ReadWriteComponent> _components = new List<ReadWriteComponent>();
    public string NiceArchetypeName => ObjectNames.NicifyVariableName(_archetype.name);

    public List<ReadWriteComponent> Components => _components;
    private Entity _prefab;

    public Entity Instantiate(string name)
    {
        var manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        return manager.Instantiate(GetPrefab(World.DefaultGameObjectInjectionWorld.EntityManager, name));
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        foreach (var component in Components)
            component.Component.AuthorComponent(entity, dstManager);
    }

    public void Update(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        foreach (var component in Components)
            component.Component.UpdateComponent(entity, dstManager);
    }

    public Entity GetPrefab(EntityManager manager, string name)
    {
        //TODO: This causes lots of problems when domain reload is off
        //Since this could reference another entity next play as it doesn't get reset.
        if (manager.Exists(_prefab))
            return _prefab;
        _prefab = CreatePrefab(manager, name);
        return _prefab;
    }

    public void UpdatePrefab(EntityManager manager)
    {
        Update(_prefab, manager, null);
    }

    private Entity CreatePrefab(EntityManager manager, string name)
    {
        if (manager.Exists(_prefab))
        {
            Update(_prefab, manager, null);
            return _prefab;
        }
        else
        {
            var entity = manager.CreateEntity();
            Convert(entity, manager, null);
            manager.AddComponent<Prefab>(entity);
            manager.SetName(entity, name);
            return entity;
        }
    }

    public void ValidateComponents()
    {
        if (_archetype == null)
            return;
        if (_components == null)
            _components = new List<ReadWriteComponent>();
        //Clear all components if archetype is null
        if (_archetype == null && _components.Count > 0)
        {
            _components.Clear();
        }

        if (_archetype != null && _components != null)
        {
            foreach (var component in _components.ToList())
            {
                //Remove null components
                if (component.Component == null)
                    _components.Remove(component);
            }
            //Add components that should exist
            foreach (var component in _archetype.Components)
            {
                if (_components.Select(x => x.Component.GetType()).Where(x => x == component.GetType()).FirstOrDefault() == null)
                {
                    var newComponent = Activator.CreateInstance(component.GetType()) as ComponentAuthoringBase;
                    _components.Add(newComponent);
                }
            }

            //Remove components that shouldn't exist
            foreach (var component in _components.ToList())
            {
                if (_archetype.Components.Select(
                    x => x.GetType()).Where(x => x == component.Component.GetType()).FirstOrDefault() == null)
                {
                    _components.Remove(component);
                }
            }
            for (int i = 0; i < _components.Count; i++)
            {
                int newIndex = _archetype.Components.Select(x => x.GetType()).ToList().IndexOf(
                    _components.Select(x => x.Component.GetType()).Where(x => x == _components[i].Component.GetType()).First());
                var temp = _components[newIndex];
                _components[newIndex] = _components[i];
                _components[i] = temp;
            }
        }
        foreach (var component in _components)
            component.Component.ValidateComponent();

        if (Application.isPlaying && World.DefaultGameObjectInjectionWorld != null)
        {
            var manager = World.DefaultGameObjectInjectionWorld.EntityManager;
            if (manager.Exists(_prefab))
                UpdatePrefab(manager);
        }
    }
}
