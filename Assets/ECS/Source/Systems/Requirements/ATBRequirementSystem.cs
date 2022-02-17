using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ATBRequirementSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _commandBuffer;

    protected override void OnCreate()
    {
        _commandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _commandBuffer.CreateCommandBuffer().AsParallelWriter();
        Entities.ForEach((int entityInQueryIndex, in ATBRequirement atbRequirement, in Target target) =>
        {
            var atb = GetComponentDataFromEntity<ATB>(true)[target.Dealer];
            atb.CurrentValue -= atbRequirement.Amount;
            ecb.SetComponent(entityInQueryIndex, target.Dealer, atb);
        }).Schedule();
        _commandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}
