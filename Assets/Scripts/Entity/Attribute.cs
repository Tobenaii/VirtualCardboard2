using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Attribute")]
public class Attribute : ScriptableObject
{
    [System.Serializable]
    public class Instance
    {
        [SerializeField] private Attribute _attribute;
        [SerializeField] [ShowIf("@_attribute")] private float _maxValue;
        [SerializeField] [ShowIf("@_attribute")] private float _defaultValue;

        public float Value { get; set; }
        public float MaxValue => _maxValue;

        public void Register(EntityInstance entity)
        {
            Value = _defaultValue;
            _attribute.Register(entity, this);
        }

        public void Remove(EntityInstance entity)
        {
            _attribute.Remove(entity);
        }
    }

    private Dictionary<EntityInstance, Instance> _valueMap = new Dictionary<EntityInstance, Instance>();
    private Dictionary<EntityInstance, List<Action<float>>> _callbackMap = new Dictionary<EntityInstance, List<Action<float>>>();

    public void RegisterCallback(EntityInstance entity, System.Action<float> onValueChanged)
    {
        List<Action<float>> actions;
        if (_callbackMap.TryGetValue(entity, out actions))
            actions.Add(onValueChanged);
        else
        {
            actions = new List<Action<float>>();
            actions.Add(onValueChanged);
            _callbackMap.Add(entity, actions);
        }
    }

    public void Register(EntityInstance entity, Instance attribute)
    {
        _valueMap.Add(entity, attribute);
    }

    public void Remove(EntityInstance instance)
    {
        _valueMap.Remove(instance);
    }

    public float GetMaxValue(EntityInstance instance)
    {
        return _valueMap[instance].MaxValue;
    }

    public float this[EntityInstance instance]
    {
        get
        {
            Instance value;
            if (_valueMap.TryGetValue(instance, out value))
                return value.Value;
            Debug.LogError($"{instance.name} does not have the attribute {this.name}");
            return 0;
        }
        set
        {
            _valueMap[instance].Value = value;
            List<Action<float>> actions;
            if (_callbackMap.TryGetValue(instance, out actions))
            {
                foreach (var callback in actions)
                    callback(value);
            }
        }
    }
}
