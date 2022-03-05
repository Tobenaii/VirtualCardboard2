using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(RequirementSystemGroup))]
public class StatusEffectRequirementSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _commandBuffer;

    protected override void OnCreate()
    {
        _commandBuffer = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _commandBuffer.CreateCommandBuffer().AsParallelWriter();
        Entities.ForEach((int entityInQueryIndex, Entity entity, in Dealer dealer, in TargetStatusEffectRequirement requirement) =>
        {
            Entity target = GetComponentDataFromEntity<Target>(true)[dealer.Entity].Entity;
            var statusBuffer = GetBufferFromEntity<StatusEffect>(true)[target];
            var status = statusBuffer[(int)requirement.Type];
            if (!status.Active)
            {
                ecb.DestroyEntity(entityInQueryIndex, entity);
            }
        }).Schedule();
        _commandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}
