using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEditor;
using UnityEngine;

public class ModEntityMono : MonoBehaviour, IConvertGameObjectToEntity
{
    [InlineEditor]
    [SerializeField] private ModEntity _entity;
    [InlineProperty]
    [SerializeField] private EntityRef _group;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        if (_entity == null)
            return;
        _group.Entity = entity;
        _entity.Convert(entity, dstManager, conversionSystem);
    }

    public Entity GetEntity(World world)
    {
        using (var blob = new BlobAssetStore())
        {
            return GameObjectConversionUtility.ConvertGameObjectHierarchy(gameObject, GameObjectConversionSettings.FromWorld(world, blob));
        }
    }
}
