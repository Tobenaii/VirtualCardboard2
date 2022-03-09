using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(ActionSystemGroup))]
public class DealDamageSystem : SystemBase
{
    private EndInitializationEntityCommandBufferSystem _commandBuffer;

    protected override void OnCreate()
    {
        _commandBuffer = World.GetExistingSystem<EndInitializationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _commandBuffer.CreateCommandBuffer().AsParallelWriter();
        Entities.ForEach((int entityInQueryIndex, Entity entity, in DealDamage dealDamage, in Dealer dealer) =>
        {
            var target = GetComponentDataFromEntity<Target>(true)[dealer.Entity];
            var damage = GetComponentDataFromEntity<Damage>(true)[target.Entity];
            damage.Amount += dealDamage.Amount;
            ecb.SetComponent(entityInQueryIndex, target.Entity, damage);
            ecb.DestroyEntity(entityInQueryIndex, entity);
        }).Schedule();
        _commandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}
