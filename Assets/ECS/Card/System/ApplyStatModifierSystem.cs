using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ApplyStatModifierSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _endSimulationEntityCommandBufferSystem;

    protected override void OnCreate()
    {
        _endSimulationEntityCommandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _endSimulationEntityCommandBufferSystem.CreateCommandBuffer().AsParallelWriter();
        Entities.ForEach((int entityInQueryIndex, Entity entity, in DynamicBuffer<ApplyStatModifier> modifiers, in Target target) =>
        {
            foreach (var modifier in modifiers)
            {
                ecb.AppendToBuffer(entityInQueryIndex, target.target,
                   new StatModifier() { ModType = modifier.Type, Amount = modifier.Amount });
            }
            ecb.DestroyEntity(entityInQueryIndex, entity);
        }).ScheduleParallel();
        _endSimulationEntityCommandBufferSystem.AddJobHandleForProducer(this.Dependency);
    }
}
