using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class HandCollectionSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Hand Hand, in DynamicBuffer<HandCard> cards) =>
        {
            Hand.CurrentCount = cards.Length;
        }).ScheduleParallel();
    }
}
