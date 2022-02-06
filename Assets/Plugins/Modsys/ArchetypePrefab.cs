using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEditor;
using UnityEngine;

public abstract class ArchetypePrefab : ScriptableObject
{
    protected abstract Type authoringType { get; }
    [ListDrawerSettings(HideAddButton = true)]
    [SerializeField][HideReferenceObjectPicker] protected List<ReadWriteComponent> _components = new List<ReadWriteComponent>();
    public IEnumerable<ComponentAuthoringBase> Components => _components.Select(x => x.Component);

    public Entity CreatePrefab(EntityManager manager)
    {
        var entity = manager.CreateEntity();
        foreach (var component in _components)
            component.Component.AuthorComponent(entity, manager);
        manager.AddComponent<Prefab>(entity);
        return entity;
    }

    [Button]
    private void AddComponent()
    {
        var selector = new GenericSelector<Type>("Component Selector", TypeCache.GetTypesDerivedFrom(authoringType).Where(x => !x.IsAbstract));
        selector.SelectionConfirmed += selection =>
        {
            var type = selection.FirstOrDefault();
            if (type == null)
                return;
            var instance = Activator.CreateInstance(type) as ComponentAuthoringBase;
            _components.Add(instance);
        };
        var window = selector.ShowInPopup();
    }
}
