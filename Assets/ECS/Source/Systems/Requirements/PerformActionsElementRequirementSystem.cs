using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[UpdateBefore(typeof(PerformActionsSystem))]
public class PerformActionsElementRequirementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref PerformActions performer, in DynamicBuffer<ElementRequirement> requirements) =>
        {
            var elementBuffer = GetBufferFromEntity<Element>(true)[performer.Dealer];
            foreach (var requirement in requirements)
            {
                var element = elementBuffer[(int)requirement.Type];
                if (element.Count < requirement.Count)
                {
                    performer.Status = IPerformActions.StatusType.Failed;
                    performer.Failure = IPerformActions.FailureType.NotEnough;
                    performer.Message = element.Name;
                    return;
                }
            }
        }).Schedule();
    }
}
