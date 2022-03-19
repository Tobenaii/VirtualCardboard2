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

[CreateAssetMenu(menuName = "Modsys/Entity Template")]
public class EntityTemplate : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField, ReadOnly]
    private List<EntityData> _entities = new List<EntityData>();

    [ListDrawerSettings(HideAddButton = true, Expanded = true)]
    [SerializeField][HideReferenceObjectPicker] protected List<ReadOnlyComponent> _components = new List<ReadOnlyComponent>();
    public IEnumerable<ComponentAuthoringBase> Components => _components.Select(x => x.Component);

    [Button]
    private void AddComponent()
    {
        var picker = new ComponentPicker();
        picker.OpenAndGetInstance((instance) => _components.Add(instance));
    }

    public void Register(EntityData modEntity)
    {
        if (!_entities.Contains(modEntity))
        {
            _entities.Add(modEntity);
            PushChanges();
        }
    }

    private void PushChanges()
    {
        foreach (var component in _components.ToList())
        {
            if (component.Component == null)
                _components.Remove(component);
        }

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