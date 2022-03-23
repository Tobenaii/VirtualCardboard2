using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PerformActionOnSpawnSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _commandBuffer;

    protected override void OnCreate()
    {
        _commandBuffer = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _commandBuffer.CreateCommandBuffer().AsParallelWriter();
        Entities.ForEach((int entityInQueryIndex,
            in Dealer dealer, in RequirementStatus status, in Action action, in PerformActionOnSpawn performer) =>
        {
            if (status.Failed)
                return;
            var actionInstance = ecb.Instantiate(entityInQueryIndex, action.Prefab);
            ecb.SetComponent(entityInQueryIndex, actionInstance, dealer);
        }).ScheduleParallel();
        _commandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}
