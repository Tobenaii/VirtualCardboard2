using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[UpdateInGroup(typeof(ActionSystemGroup))]
public class ReceiveElementSystem : SystemBase
{
    private EndInitializationEntityCommandBufferSystem _commandBuffer;

    protected override void OnCreate()
    {
        _commandBuffer = World.GetOrCreateSystem<EndInitializationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _commandBuffer.CreateCommandBuffer().AsParallelWriter();
        Entities.ForEach((int entityInQueryIndex, Entity entity, in Dealer dealer, in DynamicBuffer<ReceiveElement> elements) =>
        {
            var targetBuffer = GetBufferFromEntity<Element>(false)[dealer.Entity];
            foreach (var element in elements)
            {
                var targetElement = targetBuffer[(int)element.Type];
                targetElement.Count += element.Count;
                targetBuffer[(int)element.Type] = targetElement;
            }
            ecb.DestroyEntity(entityInQueryIndex, entity);
        }).Schedule();
        _commandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}