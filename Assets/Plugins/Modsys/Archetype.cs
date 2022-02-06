using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEditor;
using UnityEngine;

public abstract class Archetype : ScriptableObject
{
    protected abstract Type authoringType { get; }
    [ListDrawerSettings(HideAddButton = true)]
    [SerializeField][HideReferenceObjectPicker] protected List<ReadOnlyComponent> _components = new List<ReadOnlyComponent>();
    public IEnumerable<ComponentAuthoringBase> Components => _components.Select(x => x.Component);

    public EntityArchetype CreateArchetype(EntityManager manager)
    {
        return manager.CreateArchetype(Components.Select(x => x.ComponentType).ToArray());
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
