using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(ActionSystemGroup))]
public class ToggleATBPoolSystem : SystemBase
{
    private EndInitializationEntityCommandBufferSystem _commandBuffer;

    protected override void OnCreate()
    {
        _commandBuffer = World.GetOrCreateSystem<EndInitializationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _commandBuffer.CreateCommandBuffer().AsParallelWriter();
        Entities.ForEach((int entityInQueryIndex, in ToggleATBPool toggle, in Dealer dealer) =>
        {
            var atbPool = GetComponentDataFromEntity<ATBPool>(true)[dealer.Entity];
            atbPool.Enabled = toggle.Enable;
            ecb.SetComponent(entityInQueryIndex, dealer.Entity, atbPool);
        }).ScheduleParallel();
        _commandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}
