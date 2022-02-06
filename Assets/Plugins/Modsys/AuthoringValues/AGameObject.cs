using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[System.Serializable]
public class AGameObject : AuthoringValue<GameObject, Entity>
{
    public override Entity Author(World world)
    {
        Entity entity = GameObjectConversionUtility.ConvertGameObjectHierarchy(_value,
                    GameObjectConversionSettings.FromWorld(world, new BlobAssetStore()));
        return entity;
    }
}
