using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Serialization;

public interface IPerformActions
{
    public enum StatusType { Success, Failed }
    public enum FailureType { NotEnough,
        NotFound
    }
    public FixedString128 Message { get; set; }
    public StatusType Status { get; set; }
    public FailureType Failure { get; set; }
}

public struct PerformActions : IPerformActions, IComponentData
{
    public FixedString128 Message { get; set; }
    public IPerformActions.StatusType Status { get; set; }
    public IPerformActions.FailureType Failure { get; set; }
    public Entity Dealer { get; set; }
}

[InternalBufferCapacity(5)]
public struct Action : IBufferElementData
{
    public Entity Entity { get; set; }
}

public class ActionsComponent : BufferComponentAuthoring<Action>
{
    [SerializeField] private List<EntityAuthoring> _actions;

    public override void AuthorDependencies(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponent<PerformActions>(entity);
    }

    protected override NativeArray<Action> AuthorComponent(World world)
    {
        var array = new NativeArray<Action>(_actions.Count, Allocator.Temp);
        for (int i = 0; i < _actions.Count; i++)
        {
            var actionEntity = _actions[i].GetPrefab(world.EntityManager);
            world.EntityManager.AddComponent<Target>(actionEntity);
            array[i] = new Action() { Entity = _actions[i].GetPrefab(world.EntityManager) };
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
