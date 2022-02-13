using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(DealerResolverGroup))]
public class ATBCheckSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _endSimulationEcbSystem;

    protected override void OnCreate()
    {
        _endSimulationEcbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();
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
                ecb.SetComponent<ATB>(entityInQueryIndex, playCard.Dealer, dealerAtb);
            }
        }).Schedule();
        _endSimulationEcbSystem.AddJobHandleForProducer(this.Dependency);
    }
}
