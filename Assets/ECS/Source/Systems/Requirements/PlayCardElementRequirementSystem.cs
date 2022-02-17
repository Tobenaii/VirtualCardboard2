using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[UpdateBefore(typeof(PlayCardSystem))]
public class PlayCardElementRequirementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        BufferFromEntity<ElementRequirement> buffersOfAllEntities
       = this.GetBufferFromEntity<ElementRequirement>(true);
        Entities.ForEach((ref ActionStatus status, in PlayCard playCard) =>
        {
            if (!buffersOfAllEntities.HasComponent(playCard.Card))
                return;
            var elementBuffer = GetBufferFromEntity<Element>(true)[playCard.Dealer];
            var elementRequirementBuffer = GetBufferFromEntity<ElementRequirement>(true)[playCard.Card];
            foreach (var requirement in elementRequirementBuffer)
            {
                var element = elementBuffer[(int)requirement.Type];
                if (element.Count < requirement.Amount)
                {
                    status.Status = IActionStatus.StatusType.Failed;
                    status.Message = $"Not Enough {element.Name}";
                }
            }
        }).Schedule();
    }
}
