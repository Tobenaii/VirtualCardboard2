using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(ActionResolverGroup))]
public class DealDamageSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _endSimulationEcbSystem;

    protected override void OnCreate()
    {
        _endSimulationEcbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();
        Entities.ForEach((int entityInQueryIndex, Entity entity, in Target target, in DealDamage dealDamage) =>
        {
            var targetHealth = GetComponentDataFromEntity<Health>(true)[target.target];
            targetHealth.CurrentValue -= dealDamage.amount;
            ecb.SetComponent<Health>(entityInQueryIndex, target.target, targetHealth);
            ecb.DestroyEntity(entityInQueryIndex, entity);
        }).ScheduleParallel();
        _endSimulationEcbSystem.AddJobHandleForProducer(this.Dependency);

    }
}
