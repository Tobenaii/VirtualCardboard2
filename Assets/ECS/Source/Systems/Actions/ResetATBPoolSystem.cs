using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[UpdateInGroup(typeof(ActionSystemGroup))]
public class ResetATBPoolSystem : SystemBase
{
    private EndInitializationEntityCommandBufferSystem _commandBuffer;

    protected override void OnCreate()
    {
        _commandBuffer = World.GetOrCreateSystem<EndInitializationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _commandBuffer.CreateCommandBuffer().AsParallelWriter();
        Entities.ForEach((int entityInQueryIndex, Entity entity, in Dealer dealer, in ResetATBPool reset) =>
        {
            var targetPool = GetComponentDataFromEntity<ATBPool>(true)[dealer.Entity];
            targetPool.CurrentCount = targetPool.MaxCount;
            targetPool.ChargeTimer = 0;
            targetPool.Enabled = true;
            ecb.SetComponent(entityInQueryIndex, dealer.Entity, targetPool);
            ecb.DestroyEntity(entityInQueryIndex, entity);
        }).Schedule();
        _commandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}