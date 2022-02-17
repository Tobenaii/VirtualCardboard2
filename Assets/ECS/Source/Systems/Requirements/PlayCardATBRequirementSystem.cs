using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateBefore(typeof(PlayCardSystem))]
public class PlayCardATBRequirementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref ActionStatus status, in PlayCard playCard) =>
        {
            if (!HasComponent<ATBRequirement>(playCard.Card))
                return;
            var atb = GetComponentDataFromEntity<ATB>(true)[playCard.Dealer];
            var atbRequirement = GetComponentDataFromEntity<ATBRequirement>(true)[playCard.Card];
            if (atb.CurrentValue < atbRequirement.Amount)
            {
                status.Status = IActionStatus.StatusType.Failed;
                status.Message = "Not Enough ATB";
            }
        }).ScheduleParallel();
    }
}
