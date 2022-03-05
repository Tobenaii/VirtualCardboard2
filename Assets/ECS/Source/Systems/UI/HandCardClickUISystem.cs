using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class HandCardClickUISystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((int entityInQueryIndex, HandCardClickUI ui) =>
        {
            if (!EntityManager.GetChunk(ui.Target).DidChange(GetBufferTypeHandle<HandCard>(true), LastSystemVersion))
                return;
            var handCards = GetBufferFromEntity<HandCard>(true)[ui.Target];
            if (entityInQueryIndex >= handCards.Length)
                return;
            var card = handCards[entityInQueryIndex];
            ui.CardClickEvent.SetCard(card.Entity);
        }).WithoutBurst().Run();
    }
}
