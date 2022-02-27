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

[CreateAssetMenu(menuName = "Modsys/Archetype")]
public class ModArchetype : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] [ReadOnly] private List<ModEntity> _entities;
    [ListDrawerSettings(HideAddButton = true)]
    [SerializeField][HideReferenceObjectPicker] protected List<ReadOnlyComponent> _components = new List<ReadOnlyComponent>();
    public IEnumerable<ComponentAuthoringBase> Components => _components.Select(x => x.Component);

    [Button]
    private void AddComponent()
    {
        IEnumerable<Type> list = TypeCache.GetTypesDerivedFrom(typeof(ComponentAuthoring<>));
        list = list.Concat(TypeCache.GetTypesDerivedFrom(typeof(BufferComponentAuthoring<>)));
        list = list.Where(x => !x.IsAbstract);
        var selector = new GenericSelector<Type>("Component Selector", list);

        selector.SelectionConfirmed += selection =>
        {
            var type = selection.FirstOrDefault();
            if (type == null)
                return;
            var instance = Activator.CreateInstance(type);
            _components.Add((ComponentAuthoringBase)instance);
        };
        var window = selector.ShowInPopup();
    }

    public void Register(ModEntity modEntity)
    {
        if (_entities == null)
            return;
        if (!_entities.Contains(modEntity))
        {
            _entities.Add(modEntity);
            PushChanges();
        }
    }

    private void PushChanges()
    {
        if (_entities == null)
            return;
        foreach (var entity in _entities.ToList())
        {
            if (entity == null)
                _entities.Remove(entity);
            else
                entity.ValidateComponents();
        }
    }

    private void OnValidate()
    {
        PushChanges();
    }

    public void OnBeforeSerialize()
    {
        PushChanges();
    }

    public void OnAfterDeserialize()
    {
    }
}