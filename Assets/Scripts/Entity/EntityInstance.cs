using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityInstance : MonoBehaviour
{
    [SerializeField] private Entity _entity;
    [SerializeField] private int _key;
    [SerializeField] private Transform _targetingPoint;

    public Transform TargetingPoint => _targetingPoint;

    public string Name => _entity.name;

    private void Awake()
    {
        _entity.Register(this, _key);
    }

    private void OnDestroy()
    {
        _entity.Remove(this);
    }
}
