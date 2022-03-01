using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(PresentationSystemGroup))]
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
        Entities.ForEach((int entityInQueryIndex, Entity entity, ref DynamicBuffer<Action> actions, in PerformActions performer) =>
        {
            if (performer.NotReady)
                return;
            if (performer.Status == IPerformActions.StatusType.Success)
            {
                for (int i = 0; i < actions.Length; i++)
                {
                    var action = actions[i];
                    var actionInstance = ecb.Instantiate(entityInQueryIndex, actions[i].Entity);
                    var actionTarget = GetComponent<Target>(action.Entity);
                    actionTarget.Dealer = performer.Dealer;
                    var dealerTarget = GetComponent<Target>(performer.Dealer);
                    actionTarget.TargetEntity = dealerTarget.TargetEntity;
                    ecb.SetComponent(entityInQueryIndex, actionInstance, actionTarget);
                    ecb.AddComponent(entityInQueryIndex, actionInstance, new PerformActions() { Dealer = performer.Dealer });
                    if (!performer.IsContinuous)
                    {
                        actions.RemoveAt(i);
                        i--;
                    }
                }
            }
            if ((actions.Length == 0 || performer.Status == IPerformActions.StatusType.Failed) && !performer.IsContinuous)
                ecb.DestroyEntity(entityInQueryIndex, entity);
            else
                ecb.SetComponent(entityInQueryIndex, entity, new PerformActions() { Dealer = performer.Dealer});
        }).ScheduleParallel();
        _commandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}
