using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEditor;
using UnityEngine;

public class ArchetypeAuthoringMono : MonoBehaviour, IConvertGameObjectToEntity
{
    [InlineEditor]
    [SerializeField] private ModEntity _entity;
    [InlineProperty]
    [SerializeField] private EntityRef _group;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        _entity.Convert(entity, dstManager, conversionSystem);
        _group.Entity = entity;
    }

    public Entity GetEntity(World world)
    {
        using (var blob = new BlobAssetStore())
        {
            return GameObjectConversionUtility.ConvertGameObjectHierarchy(gameObject, GameObjectConversionSettings.FromWorld(world, blob));
        }
    }
}
