using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class ConsumeATBSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _commandBuffer;

    protected override void OnCreate()
    {
        _commandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _commandBuffer.CreateCommandBuffer().AsParallelWriter();
        Entities.ForEach((int entityInQueryIndex, Entity entity, in Target target, in ConsumeATB consume) =>
        {
            var targetATB = GetComponentDataFromEntity<ATB>(true)[target.Dealer];
            targetATB.CurrentValue -= consume.Amount;
            targetATB.CurrentValue = math.max(targetATB.CurrentValue, 0);
            ecb.SetComponent(entityInQueryIndex, target.Dealer, targetATB);
            ecb.DestroyEntity(entityInQueryIndex, entity);
        }).ScheduleParallel();
        _commandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}
