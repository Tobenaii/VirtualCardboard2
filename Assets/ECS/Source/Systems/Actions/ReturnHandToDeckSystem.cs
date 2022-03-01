using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateBefore(typeof(DrawCardsSystem))]
[UpdateInGroup(typeof(ActionSystemGroup))]
public class ReturnHandToDeckSystem : SystemBase
{
    private EndInitializationEntityCommandBufferSystem _endSimulationEcbSystem;

    protected override void OnCreate()
    {
        _endSimulationEcbSystem = World.GetOrCreateSystem<EndInitializationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();
        Entities.ForEach((int entityInQueryIndex, Entity entity, in ReturnHandToDeck returnHand, in Target target) => {
            var deck = GetBufferFromEntity<DeckCard>(false)[target.Dealer];
            var cardHand = GetBufferFromEntity<HandCard>(false)[target.Dealer];
            for (int i = 0; i < cardHand.Length; i++)
            {
                Debug.Log("Returning Card");
                deck.Add(new DeckCard() { Entity = cardHand[i].Entity });
            }
            cardHand.Clear();
            ecb.DestroyEntity(entityInQueryIndex, entity);
        }).Schedule();
        _endSimulationEcbSystem.AddJobHandleForProducer(this.Dependency);
        CompleteDependency();
    }
}
