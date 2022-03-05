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

public abstract class ManagedComponentAuthoring<T> : ComponentAuthoringBase where T : class, IComponentData
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
