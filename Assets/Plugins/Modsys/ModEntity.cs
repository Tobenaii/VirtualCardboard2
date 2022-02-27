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

    public void Instantiate()
    {
        _authoring.Instantiate();
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        _authoring.Convert(entity, dstManager, conversionSystem);
    }

    public Entity GetPrefab(EntityManager manager)
    {
        var ecb = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>().CreateCommandBuffer();
        return _authoring.GetPrefab(manager);
    }

    [ShowIf("@UnityEngine.Application.isPlaying")] [PropertyOrder(100000)]
    [Button("Test")]
    private void DebugInstantiate()
    {
        Instantiate();
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
    [ShowInInspector] public ModArchetype Archetype => _archetype;
    [ShowIf("@_archetype == null")]
    [SerializeField] private ModArchetype _archetype;
    [SerializeField]
    [ListDrawerSettings(IsReadOnly = true, Expanded = true, ShowItemCount = false)]
    [HideReferenceObjectPicker]
    [ShowIf("@_archetype != null")]
    [LabelText("@_niceArchetypeName")]
    private List<ReadWriteComponent> _components = new List<ReadWriteComponent>();
    private string _niceArchetypeName => ObjectNames.NicifyVariableName(_archetype.name);

    public List<ReadWriteComponent> Components => _components;
    private Entity _prefab;

    public void Instantiate()
    {
        var ecb = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>().CreateCommandBuffer();
        ecb.Instantiate(GetPrefab(World.DefaultGameObjectInjectionWorld.EntityManager));
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

    public Entity GetPrefab(EntityManager manager)
    {
        //TODO: This causes lots of problems when domain reload is off
        //Since this could reference another entity next play as it doesn't get reset.
        if (manager.Exists(_prefab))
            return _prefab;
        _prefab = CreatePrefab(manager);
        return _prefab;
    }

    private Entity CreatePrefab(EntityManager manager)
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

        if (Application.isPlaying)
        {
            var manager = World.DefaultGameObjectInjectionWorld.EntityManager;
            if (manager.Exists(_prefab))
                _prefab = CreatePrefab(manager);
        }
    }
}
