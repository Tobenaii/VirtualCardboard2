using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ElementRequirementSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _commandBuffer;

    protected override void OnCreate()
    {
        _commandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _commandBuffer.CreateCommandBuffer().AsParallelWriter();
        Entities.ForEach((int entityInQueryIndex, in DynamicBuffer<ElementRequirement> elementRequirement, in Target target) =>
        {
            foreach (var requirement in elementRequirement)
            {
                var elementBuffer = GetBufferFromEntity<Element>(false)[target.Dealer];
                var element = elementBuffer[(int)requirement.Type];
                element.Count -= requirement.Amount;
                elementBuffer[(int)requirement.Type] = element;
            }

        }).Schedule();
        _commandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}
