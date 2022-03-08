using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(RequirementSystemGroup))]
public class ElementRequirementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref RequirementStatus status, in Dealer dealer, in DynamicBuffer<ElementRequirement> requirements) =>
        {
            var elementBuffer = GetBufferFromEntity<Element>(true)[dealer.Entity];
            foreach (var requirement in requirements)
            {
                var element = elementBuffer[(int)requirement.Type];
                if (element.Count < requirement.Count)
                {
                    status.Failed = true;
                }
            }
        }).Schedule();
    }
}
