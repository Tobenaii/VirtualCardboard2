using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(ActionSystemGroup))]
public class ApplyStatusEffectSystem : SystemBase
{
    private EndInitializationEntityCommandBufferSystem _commandBuffer;

    protected override void OnCreate()
    {
        _commandBuffer = World.GetOrCreateSystem<EndInitializationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _commandBuffer.CreateCommandBuffer().AsParallelWriter();
        Entities.ForEach((ref ApplyStatusEffect effect, in Target target) =>
        {
            var buffer = GetBufferFromEntity<StatusEffect>(false)[target.Entity];
            var status = buffer[effect.Type];
            status.Active = true;
            buffer[effect.Type] = status;

        }).Schedule();
        _commandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}
