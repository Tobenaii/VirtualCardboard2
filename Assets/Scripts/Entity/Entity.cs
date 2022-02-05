using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[InlineProperty]
public class EntityRef
{
    [HorizontalGroup]
    [HideLabel]
    [SerializeField] private Entity _entity;
    [HorizontalGroup] [HideLabel]
    [SerializeField] private int _key;

    public EntityInstance Instance => _entity[_key];
}

[CreateAssetMenu(menuName = "VC2/Entity")]
public class Entity : ScriptableObject
{
    [SerializeField] private List<Attribute.Instance> _attributeDefaults = new List<Attribute.Instance>();

    private Dictionary<int, EntityInstance> _entityInstances = new Dictionary<int,EntityInstance>();

    public void Register(EntityInstance instance, int key)
    {
        if (_entityInstances.ContainsKey(key))
        {
            Remove(_entityInstances[key]);
            _entityInstances[key] = instance;
        }
        else
            _entityInstances.Add(key, instance);

        foreach (var attribute in _attributeDefaults)
            attribute.Register(instance);
    }

    public void Remove(EntityInstance instance)
    {
        foreach (var attribute in _attributeDefaults)
            attribute.Remove(instance);
    }

    public EntityInstance this[int key]
    {
        get
        {
            return _entityInstances[key];
        }
    }
}
