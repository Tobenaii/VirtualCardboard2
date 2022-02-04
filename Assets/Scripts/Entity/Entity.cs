using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : ScriptableObject
{
    [SerializeField] private List<Attribute> _attributes;

    private Dictionary<Attribute, float> _attributeMap;

    public string Name { get; }

    public float GetAttribute(Attribute attribute)
    {
        return _attributeMap[attribute];
    }

    public void SetAttribute(Attribute attribute, float value)
    {
        Debug.Log($"Set {Name} {attribute} to {value}");
        _attributeMap[attribute] = value;
    }
}
