using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(RequirementSystemGroup))]
public class ElementRequirementSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _commandBuffer;

    protected override void OnCreate()
    {
        _commandBuffer = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _commandBuffer.CreateCommandBuffer().AsParallelWriter();
        Entities.ForEach((Entity entity, int entityInQueryIndex, in Dealer dealer,
                            in DynamicBuffer<ElementRequirement> requirements) =>
        {
            var elementBuffer = GetBufferFromEntity<Element>(true)[dealer.Entity];
            foreach (var requirement in requirements)
            {
                var element = elementBuffer[(int)requirement.Type];
                if (element.Count < requirement.Count)
                {
                    ecb.DestroyEntity(entityInQueryIndex, entity);
                }
            }
        }).Schedule();
        _commandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}
