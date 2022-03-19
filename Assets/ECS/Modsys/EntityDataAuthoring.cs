using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;


[System.Serializable, InlineProperty, HideLabel, HideReferenceObjectPicker]
public class EntityDataAuthoring
{
    public List<ReadWriteComponent> Components => _components;

    [ShowIf("@_template != null"), PropertyOrder(-1000), HideLabel, ShowInInspector]
    public EntityTemplate Template => _template;

    [ShowIf("@_template == null"), SerializeField, FormerlySerializedAs("_archetype")]
    private EntityTemplate _template;

    [SerializeField, HideReferenceObjectPicker, ListDrawerSettings(IsReadOnly = true, Expanded = true, ShowItemCount = false), ShowIf("@_template != null"), LabelText("@_niceArchetypeName")]
    private List<ReadWriteComponent> _components = new List<ReadWriteComponent>();

    private string _niceArchetypeName => ObjectNames.NicifyVariableName(_template.name);

    public Entity Convert(Entity entity, EntityManager dstManager)
    {
        foreach (var component in Components)
            component.Component.AuthorComponent(entity, dstManager);
        return entity;
    }

    public void Update(Entity entity, EntityManager dstManager)
    {
        foreach (var component in Components)
            component.Component.UpdateComponent(entity, dstManager);
    }

    public Entity CreatePrefab(EntityManager manager, string name)
    {
        var entity = manager.CreateEntity();
        Convert(entity, manager);
        manager.AddComponent<Prefab>(entity);
        manager.SetName(entity, name);
        return entity;
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
    }
}

