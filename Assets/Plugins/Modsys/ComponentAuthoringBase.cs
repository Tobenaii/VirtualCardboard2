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

public abstract class ComponentAuthoringBase : ScriptableObject
{
    public abstract ComponentType ComponentType { get; }
    public abstract void AuthorComponent(Entity entity, EntityManager dstManager);
    public virtual void AuthorDependencies(Entity entity, EntityManager dstManager) { }
}

public abstract class ComponentAuthoring<T> : ComponentAuthoringBase where T : struct, IComponentData
{
    public override ComponentType ComponentType => new ComponentType(typeof(T));
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, AuthorComponent(dstManager.World));
        AuthorDependencies(entity, dstManager);
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
        if (components.Length > 0)
            buffer.AddRange(components);
        AuthorDependencies(entity, dstManager);
    }
    protected abstract NativeArray<T> AuthorComponent(World world);
}    


[System.Serializable]
[InlineProperty]
[HideLabel]
[HideReferenceObjectPicker]
public class ArchetypeReference
{
    [ShowIf("@_archetype == null")]
    [SerializeField] private Archetype _archetype;
    [SerializeField]
    [ListDrawerSettings(IsReadOnly = true)]
    [HideReferenceObjectPicker]
    [ShowIf("@_archetype != null")]
    private List<ReadWriteComponent> _components;
    
    public List<ReadWriteComponent> Components => _components;
    public Archetype Archetype => _archetype;

    public void ValidateComponents(ScriptableObject entity)
    {
        if (_archetype == null)
            return;
        if (_components == null)
            _components = new List<ReadWriteComponent>();
        //Clear all components if archetype is null
        if (_archetype == null && _components.Count > 0)
        {
            foreach (var asset in AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(entity)))
            {
                if (asset == entity)
                    continue;
                _components.Remove(_components.FirstOrDefault(x => x.Component == asset));
                UnityEngine.Object.DestroyImmediate(asset);
            }
            _components.Clear();
        }

        if (_archetype != null && _components != null)
        {   
            foreach (var component in _components.ToList())
            {
                if (component.Component == null)
                    _components.Remove(component);
            }    
            //Add components that should exist
            foreach (var component in _archetype.Components)
            {
                if (_components.Select(x => x.Component.GetType()).Where(x => x == component.GetType()).FirstOrDefault() == null)
                {
                    var newComponent = ScriptableObject.CreateInstance(component.GetType()) as ComponentAuthoringBase;
                    _components.Add(newComponent);
                    AssetDatabase.AddObjectToAsset(newComponent, entity);
                    AssetDatabase.Refresh();
                }
            }

            //Remove components that shouldn't exist
            foreach (var component in _components.ToList())
            {
                if (_archetype.Components.Select(
                    x => x.GetType()).Where(x => x == component.Component.GetType()).FirstOrDefault() == null)
                {
                    _components.Remove(component);
                    UnityEngine.Object.DestroyImmediate(component.Component, true);
                    AssetDatabase.Refresh();
                }
            }

            //Remove all subassets that shouldn't exist (the above should handle this in most cases)
            foreach (var asset in AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(entity)))
            {
                if (asset == entity)
                    continue;
                if (asset == null || !_components.Select(x => x.Component.GetType()).Contains(asset.GetType()))
                    UnityEngine.Object.DestroyImmediate(asset, true);
            }
        }
    }
}


[System.Serializable]
public class ReadOnlyComponent
{
    [HideReferenceObjectPicker]
    [Sirenix.OdinInspector.ReadOnly]
    [HideLabel]
    [InlineEditor]
    [SerializeField] private ComponentAuthoringBase _component;
    public ComponentAuthoringBase Component => _component;
    public static implicit operator ReadOnlyComponent(ComponentAuthoringBase component)
        => new ReadOnlyComponent() { _component = component };
}

[System.Serializable]
public class ReadWriteComponent
{
    [LabelText("@_component.GetType()")]
    [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.CompletelyHidden, DrawHeader = true)]
    [SerializeField] private ComponentAuthoringBase _component;
    public ComponentAuthoringBase Component => _component;
    public static implicit operator ReadWriteComponent(ComponentAuthoringBase component)
        => new ReadWriteComponent() { _component = component };
}
