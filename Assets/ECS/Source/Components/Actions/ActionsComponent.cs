using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Serialization;

public struct Dealer : IComponentData
{
    public Entity Entity { get; set; }
}


[InternalBufferCapacity(5)]
public struct Action : IBufferElementData
{
    public Entity Entity { get; set; }
}

public class ActionsComponent : BufferComponentAuthoring<Action>
{
    [ListDrawerSettings(Expanded = true, ShowItemCount = false)]
    [SerializeField] private List<EntityAuthoring> _actions;

    protected override NativeArray<Action> AuthorComponent(World world)
    {
        var array = new NativeArray<Action>(_actions.Count, Allocator.Temp);
        for (int i = 0; i < _actions.Count; i++)
        {
            var actionEntity = _actions[i].GetPrefab(world.EntityManager, _actions[i].NiceArchetypeName);
            world.EntityManager.AddComponent<Target>(actionEntity);
            array[i] = new Action() { Entity = _actions[i].GetPrefab(world.EntityManager, _actions[i].NiceArchetypeName) };
        }
        return array;
    }

    public override void ValidateComponent()
    {
        if (_actions == null)
            return;
        foreach (var action in _actions)
        {
            action.ValidateComponents();
        }
    }
}
