using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using UnityEditor;
using UnityEngine;

public abstract class ComponentAuthoringBase
{
    public abstract ComponentType ComponentType { get; }
    public abstract void AuthorComponent(Entity entity, EntityManager dstManager);
    public virtual void AuthorDependencies(Entity entity, EntityManager dstManager) { }
    public virtual void ValidateComponent() { }
    public abstract void UpdateComponent(Entity entity, EntityManager dstManager);
}

public abstract class ComponentAuthoring<T> : ComponentAuthoringBase where T : struct, IComponentData
{
    public override ComponentType ComponentType => new ComponentType(typeof(T));
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, AuthorComponent(dstManager.World));
        AuthorDependencies(entity, dstManager);
    }
    public override void UpdateComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.SetComponentData(entity, AuthorComponent(dstManager.World));
    }
    protected abstract T AuthorComponent(World world);
}

public abstract class BufferComponentAuthoring<T> : ComponentAuthoringBase where T : struct, IBufferElementData
{
    public override ComponentType ComponentType => new ComponentType(typeof(T));
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        var components = AuthorComponent(dstManager.World);
        var buffer = dstManager.AddBuffer<T>(entity);
        buffer.AddRange(components);
        AuthorDependencies(entity, dstManager);
    }
    public override void UpdateComponent(Entity entity, EntityManager dstManager)
    {
        var buffer = dstManager.GetBuffer<T>(entity);
        buffer.Clear();
        buffer.AddRange(AuthorComponent(dstManager.World));
    }
    protected abstract NativeArray<T> AuthorComponent(World world);
}    


[System.Serializable]
[InlineProperty]
[HideLabel]
[HideReferenceObjectPicker]
public class ArchetypeAuthoring
{
    [ShowIf("@_archetype != null")]
    [PropertyOrder(-1000)]
    [HideLabel]
    [ShowInInspector] public Archetype Archetype => _archetype;
    [ShowIf("@_archetype == null")]
    [SerializeField] private Archetype _archetype;
    [SerializeField]
    [ListDrawerSettings(IsReadOnly = true)]
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


[System.Serializable]
public class ReadOnlyComponent
{
    [Sirenix.OdinInspector.ReadOnly]
    [LabelText("@_name")]
    [SerializeReference] private ComponentAuthoringBase _component;
    private string _name => ObjectNames.NicifyVariableName(_component.GetType().Name.Replace("Component", ""));

    public ComponentAuthoringBase Component => _component;
    public static implicit operator ReadOnlyComponent(ComponentAuthoringBase component)
        => new ReadOnlyComponent() { _component = component };
}

[System.Serializable]
public class ReadWriteComponent
{
    [LabelText("@_name")]
    [HideReferenceObjectPicker]
    [SerializeReference] private ComponentAuthoringBase _component;
    private string _name => ObjectNames.NicifyVariableName(_component.GetType().Name.Replace("Component", ""));
    public ComponentAuthoringBase Component => _component;
    public static implicit operator ReadWriteComponent(ComponentAuthoringBase component)
        => new ReadWriteComponent() { _component = component };
}
