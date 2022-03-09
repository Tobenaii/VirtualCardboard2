using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Modsys/Entity")]
public class ModEntity : ScriptableObject, ISerializationCallbackReceiver
{
    [PropertyOrder(10000)]
    [SerializeField] private EntityAuthoring _authoring;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        _authoring.Convert(entity, dstManager);
    }

    public Entity GetPrefab(EntityManager manager, string name)
    {
        var ecb = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>().CreateCommandBuffer();
        return _authoring.GetPrefab(manager, name);
    }

    private void Register()
    {
        if (_authoring != null)
            _authoring.Template?.Register(this);
    }

    private void OnValidate()
    {
        Register();
        ValidateComponents();
    }

    public void ValidateComponents()
    {
        if (_authoring.Template != null)
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
    public List<ReadWriteComponent> Components => _components;

    [ShowIf("@_template != null"), PropertyOrder(-1000), HideLabel, ShowInInspector]
    public EntityTemplate Template => _template;

    [ShowIf("@_template == null"), SerializeField, FormerlySerializedAs("_archetype")]
    private EntityTemplate _template;

    [SerializeField, HideReferenceObjectPicker, ListDrawerSettings(IsReadOnly = true, Expanded = true, ShowItemCount = false), ShowIf("@_template != null"), LabelText("@_niceArchetypeName")]
    private List<ReadWriteComponent> _components = new List<ReadWriteComponent>();

    private string _niceArchetypeName => ObjectNames.NicifyVariableName(_template.name);
    private Entity _prefab;

    public void Convert(Entity entity, EntityManager dstManager)
    {
        foreach (var component in Components)
            component.Component.AuthorComponent(entity, dstManager);
    }

    public void Update(EntityManager dstManager)
    {
        foreach (var component in Components)
            component.Component.UpdateComponent(_prefab, dstManager);
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

    private Entity CreatePrefab(EntityManager manager, string name)
    {
        if (manager.Exists(_prefab))
        {
            Update(manager);
            return _prefab;
        }
        else
        {
            var entity = manager.CreateEntity();
            Convert(entity, manager);
            manager.AddComponent<Prefab>(entity);
            manager.SetName(entity, name);
            return entity;
        }
    }

    public void ValidateComponents()
    {
        if (_template == null)
            return;

        var templateTypes = _template.Components.Select(x => x.GetType());
        var entityTypes = _components.Select(x => x.Component.GetType());

        //Remove all null components
        _components.RemoveAll(x => x.Component == null);

        //Add components from template
        _components.AddRange(
            templateTypes.Where(x => !entityTypes.Contains(x))
            .Select(newType => (ComponentAuthoringBase)Activator.CreateInstance(newType))
            .Select(comp => (ReadWriteComponent)comp));

        //Remove components not in template anymore
        _components.RemoveAll(x =>
            !templateTypes.Contains(x.Component.GetType())
            );

        //Order components based on template
        Dictionary<Type, ReadWriteComponent> sorter = _components.ToDictionary(x => x.Component.GetType());
        _components = templateTypes.Select(x => sorter[x]).ToList();

        foreach (var component in _components)
            component.Component.ValidateComponent();

        if (Application.isPlaying && World.DefaultGameObjectInjectionWorld != null)
        {
            var manager = World.DefaultGameObjectInjectionWorld.EntityManager;
            if (manager.Exists(_prefab))
                Update(manager);
        }
    }
}
