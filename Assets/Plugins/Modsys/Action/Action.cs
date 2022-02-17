using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(menuName = "Modsys/Action")]
public class Action : ScriptableObject
{
    public virtual Entity Execute<T>(T data) where T : struct, IComponentData
    {
        var entity = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntity();
        World.DefaultGameObjectInjectionWorld.EntityManager.AddComponentData<T>(entity, data);
        World.DefaultGameObjectInjectionWorld.EntityManager.AddComponentData<ActionStatus>(entity, new ActionStatus());
        return entity;
    }
}
