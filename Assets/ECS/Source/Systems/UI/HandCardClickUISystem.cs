using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class HandCardClickUISystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _commandBuffer;

    protected override void OnCreate()
    {
        _commandBuffer = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _commandBuffer.CreateCommandBuffer();
        Entities.ForEach((int entityInQueryIndex, HandCardClickUI ui, in Dealer dealer) =>
        {
            if (!ui.CardClickEvent.HasClicked)
                return;
            var handCards = GetBufferFromEntity<HandCard>(true)[dealer.Entity];
            if (entityInQueryIndex >= handCards.Length)
                return;
            var card = handCards[entityInQueryIndex];
            var entity = ecb.Instantiate(card.Entity);
        }).WithoutBurst().Run();
        _commandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}
