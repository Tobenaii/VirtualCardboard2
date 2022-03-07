using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class DeckCollectionSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Deck deck, in DynamicBuffer<DeckCard> cards) =>
        {
            deck.CurrentCount = cards.Length;
        }).ScheduleParallel();
    }
}