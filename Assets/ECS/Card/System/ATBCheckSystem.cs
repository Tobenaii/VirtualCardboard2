using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(DealerResolverGroup))]
public class ATBCheckSystem : SystemBase
{
    protected override void OnCreate()
    {
    }

    protected override void OnUpdate()
    {
        Entities.ForEach((int entityInQueryIndex, Entity entity, ref PlayCard playCard) =>
        {
            var dealerAtb = GetComponentDataFromEntity<ATB>(true)[playCard.Dealer];
            var requiredATB = GetComponentDataFromEntity<ConsumeATB>(false)[playCard.Card];
            if (dealerAtb.Charges < requiredATB.amount)
            {
                playCard.Failed = true;
                playCard.Status = IStatusMessage.StatusType.Failed;
                playCard.Message = "Not Enough ATB";
            }
            else
            {
                dealerAtb.Charges -= requiredATB.amount;
                SetComponent<ATB>(playCard.Dealer, dealerAtb);
            }
        }).Schedule();
    }
}
