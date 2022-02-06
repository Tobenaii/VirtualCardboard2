using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public interface IComponentAuthoring<T, V> : IComponentData where T : struct, IComponentData where V : ComponentAuthoring<T>
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, this);
    }
}

public interface IManagedComponentAuthoring<T, V> : IComponentData where T : IComponentData where V : ManagedComponentAuthoring<T>
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponent<T>(entity);
    }
}
