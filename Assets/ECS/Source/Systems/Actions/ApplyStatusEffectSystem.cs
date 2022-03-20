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
        Entities.ForEach((Entity entity, int entityInQueryIndex, in ApplyStatusEffect effect, in Dealer dealer) =>
        {
            var target = GetComponentDataFromEntity<Target>(true)[dealer.Entity];
            var buffer = GetBufferFromEntity<StatusEffect>(false)[target.Entity];
            var status = buffer[effect.Type];
            status.Active = true;
            buffer[effect.Type] = status;
            ecb.DestroyEntity(entityInQueryIndex, entity);

        }).Schedule();
        _commandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}
