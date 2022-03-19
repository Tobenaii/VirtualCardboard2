using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Modsys/Entity Data")]
public class EntityData : ScriptableObject
{
    [PropertyOrder(10000)]
    [SerializeField] private EntityDataAuthoring _authoring;

    public Entity Convert(Entity entity, EntityManager dstManager)
    {
        return _authoring.Convert(entity, dstManager);
    }

    //public void Update(Entity entity, EntityManager dstManager)
    //{
    //    _authoring.Update(entity, dstManager);
    //}

    public void ValidateComponents()
    {
        if (_authoring.Template != null)
            _authoring.ValidateComponents();
    }

    private void OnValidate()
    {
        if (_authoring != null)
            _authoring.Template?.Register(this);
    }

    public Entity GetPrefab(EntityManager dstManager)
    {
        return default(Entity);
    }
}