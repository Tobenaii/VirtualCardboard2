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
        Entities.ForEach((int entityInQueryIndex, Entity entity, in Action action, in Dealer dealer) =>
        {
            var actionInstance = ecb.Instantiate(entityInQueryIndex, action.Prefab);
            var target = GetComponentDataFromEntity<Target>(true)[dealer.Entity];
            ecb.SetComponent(entityInQueryIndex, actionInstance, dealer);
            ecb.DestroyEntity(entityInQueryIndex, entity);
        }).ScheduleParallel();
        _commandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}
