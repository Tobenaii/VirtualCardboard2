using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class ConsumeElementSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _commandBuffer;

    protected override void OnCreate()
    {
        _commandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _commandBuffer.CreateCommandBuffer().AsParallelWriter();
        Entities.ForEach((int entityInQueryIndex, Entity entity, in Target target, in DynamicBuffer<ConsumeElement> elements) =>
        {
            var targetBuffer = GetBufferFromEntity<Element>(false)[target.Dealer];
            foreach (var element in elements)
            {
                var targetElement = targetBuffer[(int)element.Type];
                targetElement.Count -= element.Count;
                targetElement.Count = math.max(targetElement.Count, 0);
                targetBuffer[(int)element.Type] = targetElement;
            }
            ecb.DestroyEntity(entityInQueryIndex, entity);
        }).Schedule();
        _commandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}
