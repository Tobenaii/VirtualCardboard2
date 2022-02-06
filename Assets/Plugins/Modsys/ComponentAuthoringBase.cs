using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public abstract class ComponentAuthoringBase
{
    public abstract ComponentType ComponentType { get; }
    public abstract void AuthorComponent(Entity entity, EntityManager dstManager);
}

public abstract class ComponentListAuthoringBase
{
    public abstract ComponentType ComponentType { get; }
    public abstract void AuthorComponent(Entity entity, EntityManager dstManager);
}

public abstract class ComponentAuthoring<T> : ComponentAuthoringBase where T : struct, IComponentData
{
    public override ComponentType ComponentType => new ComponentType(typeof(T));
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, AuthorComponent(dstManager.World));
    }
    protected abstract T AuthorComponent(World world);
}

public abstract class ManagedComponentAuthoring<T> : ComponentAuthoringBase where T : IComponentData
{
    public override ComponentType ComponentType => new ComponentType(typeof(T));
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentObject(entity, AuthorComponent(dstManager.World));
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
    }
    protected abstract NativeArray<T> AuthorComponent(World world);
}    


[System.Serializable]
[InlineProperty]
[HideLabel]
[HideReferenceObjectPicker]
public class ArchetypeReference : ISerializationCallbackReceiver
{
    [System.Serializable]
    public class ReadWriteComponent
    {
        [SerializeReference][HideReferenceObjectPicker][LabelText("@_component.GetType().Name")] private ComponentAuthoringBase _component;
        public ComponentAuthoringBase Component => _component;

        public static implicit operator ReadWriteComponent(ComponentAuthoringBase component)
            => new ReadWriteComponent() { _component = component };
    }

    [SerializeField] private Archetype _componentData;
    [SerializeField]
    [ListDrawerSettings(IsReadOnly = true)]
    [HideReferenceObjectPicker]
    [ShowIf("@_components != null")]
    private List<ReadWriteComponent> _components;

    public List<ReadWriteComponent> Components => _components;

    private void ValidateComponents()
    {
        if (_componentData == null)
            _components = null;
        else
        {
            if (_components != null)
            {
                var typesAvailable = _componentData.Components.Select(x => x.GetType()).ToList();
                _components.RemoveAll(x => x == null || x.Component == null);
                _components.RemoveAll(x => !typesAvailable.Contains(x.Component.GetType()));
            }
            else
                _components = new List<ReadWriteComponent>();
            _components.AddRange(_componentData.Components.Select(x => x.GetType()).Except(_components.Select(x => x.Component.GetType())).Select(x => (ReadWriteComponent)(ComponentAuthoringBase)Activator.CreateInstance(x)));
        }
    }

    public Entity GetEntity(World world)
    {
        return world.EntityManager.CreateEntity(_componentData.CreateArchetype(world.EntityManager));
    }

    public void OnBeforeSerialize()
    {
        ValidateComponents();
    }

    public void OnAfterDeserialize()
    {
    }
}


[System.Serializable]
public class ReadOnlyComponent
{
    [SerializeReference][HideReferenceObjectPicker][Sirenix.OdinInspector.ReadOnly][LabelText("@_component.GetType().Name")] private ComponentAuthoringBase _component;
    public ComponentAuthoringBase Component => _component;
    public static implicit operator ReadOnlyComponent(ComponentAuthoringBase component)
        => new ReadOnlyComponent() { _component = component };
}

[System.Serializable]
public class ReadWriteComponent
{
    [SerializeReference][HideReferenceObjectPicker][LabelText("@_component.GetType().Name")] private ComponentAuthoringBase _component;
    public ComponentAuthoringBase Component => _component;
    public static implicit operator ReadWriteComponent(ComponentAuthoringBase component)
        => new ReadWriteComponent() { _component = component };
}
