using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class HandCardDataUISystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((int entityInQueryIndex, HandCardDataUI ui) =>
        {
            if (!EntityManager.GetChunk(ui.Target).DidChange(GetBufferTypeHandle<HandCard>(true), LastSystemVersion))
                return;
            var handCards = GetBufferFromEntity<HandCard>(true)[ui.Target];
            if (entityInQueryIndex >= handCards.Length)
                return;
            var card = handCards[entityInQueryIndex];
            var cardData = GetComponentDataFromEntity<CardData>(true)[card.Entity];
            ui.Title.text = cardData.name.ConvertToString();
            ui.Description.text = cardData.description.ConvertToString();
        }).WithoutBurst().Run();
    }
}
