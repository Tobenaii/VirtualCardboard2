using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ToggleATBPoolSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _commandBuffer;

    protected override void OnCreate()
    {
        _commandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _commandBuffer.CreateCommandBuffer().AsParallelWriter();
        Entities.ForEach((int entityInQueryIndex, in ToggleATBPool toggle, in Target target) =>
        {
            var atbPool = GetComponentDataFromEntity<ATBPool>(true)[target.Dealer];
            atbPool.Enabled = toggle.Enable;
            ecb.SetComponent(entityInQueryIndex, target.Dealer, atbPool);
        }).ScheduleParallel();
        _commandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}
