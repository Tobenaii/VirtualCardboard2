using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PerformActionsSystem : SystemBase
{
    private BeginInitializationEntityCommandBufferSystem _commandBuffer;

    protected override void OnCreate()
    {
        _commandBuffer = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _commandBuffer.CreateCommandBuffer().AsParallelWriter();
        Entities.WithChangeFilter<Action>().ForEach((int entityInQueryIndex, Entity entity, ref PerformActions performer, ref DynamicBuffer<Action> actions) =>
        {
            if (performer.Status == IPerformActions.StatusType.Success)
            {
                for (int i = 0; i < actions.Length; i++)
                {
                    var action = actions[i];
                    var actionInstance = ecb.Instantiate(entityInQueryIndex, actions[i].Entity);
                    if (HasComponent<Target>(action.Entity))
                    {
                        var actionTarget = GetComponent<Target>(action.Entity);
                        actionTarget.Dealer = performer.Dealer;
                        if (!actionTarget.HasTarget)
                        {
                            var dealerTarget = GetComponent<Target>(performer.Dealer);
                            if (dealerTarget.HasTarget)
                            {
                                actionTarget.TargetEntity = dealerTarget.TargetEntity;
                            }
                        }
                        ecb.SetComponent(entityInQueryIndex, actionInstance, actionTarget);
                    }
                    actions.RemoveAt(i);
                    i--;
                }
            }
            if (actions.Length == 0 || performer.Status == IPerformActions.StatusType.Failed)
                ecb.DestroyEntity(entityInQueryIndex, entity);
        }).ScheduleParallel();
        _commandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}
