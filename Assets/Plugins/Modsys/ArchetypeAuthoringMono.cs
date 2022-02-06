using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEditor;
using UnityEngine;

public class ArchetypeAuthoringMono : SerializedMonoBehaviour, IConvertGameObjectToEntity
{
    [SerializeField] private ArchetypeReference _component;
    [HorizontalGroup]
    [SerializeField] private bool _entityRef;
    [HorizontalGroup]
    [SerializeField][ShowIf("@_entityRef")][HideLabel][InlineProperty] private EntityRef _entity;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        foreach (var component in _component.Components)
            component.Component.AuthorComponent(entity, dstManager);
        if (_entityRef)
            _entity.Entity = entity;
    }

    public Entity GetEntity(World world)
    {
        using (var blob = new BlobAssetStore())
        {
            return GameObjectConversionUtility.ConvertGameObjectHierarchy(gameObject, GameObjectConversionSettings.FromWorld(world, blob));
        }
    }

    private void OnDestroy()
    {
        if (_entityRef)
            _entity.Unregister();
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Modsys/Archetype Prefab")]
    private static void CreateArchetypePrefabInEditor()
    {
        //var source = new GameObject("New Archetype");
        //PrefabUtility.SaveAsPrefabAsset(source, )
    }
#endif
}
