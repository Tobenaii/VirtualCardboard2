using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
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
    public abstract void AuthorComponent(Entity entity, EntityManager dstManager);
    public virtual void ValidateComponent() { }
    public virtual void UpdateComponent(Entity entity, EntityManager dstManager) { }
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

public class ComponentPicker
{
    public void OpenAndGetInstance(Action<ComponentAuthoringBase> callback)
    {
        IEnumerable<Type> list = TypeCache.GetTypesDerivedFrom(typeof(ComponentAuthoringBase));
        list = list.Where(x => !x.IsAbstract);
        var selector = new GenericSelector<Type>("Component Selector", list);

        selector.SelectionConfirmed += selection =>
        {
            var type = selection.FirstOrDefault();
            if (type == null)
                return;
            var instance = Activator.CreateInstance(type);
            callback((ComponentAuthoringBase)instance);
        };
        var window = selector.ShowInPopup();
    }
}