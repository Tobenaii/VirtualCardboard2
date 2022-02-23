using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ApplyStatusEffectSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _commandBuffer;

    protected override void OnCreate()
    {
        _commandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _commandBuffer.CreateCommandBuffer().AsParallelWriter();
        Entities.ForEach((ref ApplyStatusEffect effect, in Target target) =>
        {
            var buffer = GetBufferFromEntity<StatusEffect>(false)[target.TargetEntity];
            var status = buffer[effect.Type];
            status.Active = true;
            buffer[effect.Type] = status;

        }).Schedule();
        _commandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}
