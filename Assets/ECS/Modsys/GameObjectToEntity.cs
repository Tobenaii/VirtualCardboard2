using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameObjectToEntity : MonoBehaviour, IConvertGameObjectToEntity
{
    [InlineEditor]
    [SerializeField] private EntityData _entityData;
    [InlineProperty]
    [SerializeField] private EntityRef _group;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        if (_entityData == null)
            return;
        _group.Entity = entity;
        _entityData.Convert(entity, dstManager);
    }
}
