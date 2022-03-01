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
        Entities.ForEach((int entityInQueryIndex, Entity entity, in Target target, in ResetATBPool reset) =>
        {
            var targetPool = GetComponentDataFromEntity<ATBPool>(true)[target.Dealer];
            targetPool.CurrentCount = targetPool.MaxCount;
            targetPool.ChargeTimer = 0;
            targetPool.Enabled = true;
            ecb.SetComponent(entityInQueryIndex, target.Dealer, targetPool);
            ecb.DestroyEntity(entityInQueryIndex, entity);
        }).Schedule();
        _commandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}