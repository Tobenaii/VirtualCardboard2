using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class DealDamageSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _commandBuffer;

    protected override void OnCreate()
    {
        _commandBuffer = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _commandBuffer.CreateCommandBuffer().AsParallelWriter();
        Entities.ForEach((int entityInQueryIndex, Entity entity, in DealDamage dealDamage, in Target target) =>
        {
            var damage = GetComponentDataFromEntity<Damage>(true)[target.target];
            damage.Amount += dealDamage.amount;
            ecb.SetComponent(entityInQueryIndex, target.target, damage);
            ecb.DestroyEntity(entityInQueryIndex, entity);
        }).Schedule();
        _commandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}
