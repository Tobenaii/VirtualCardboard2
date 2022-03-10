using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(UISystemGroup))]
public class DeckCountUISystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((DeckCountUI ui, in Dealer dealer) =>
        {
            if (!EntityManager.GetChunk(dealer.Entity).DidChange(GetComponentTypeHandle<Deck>(true), LastSystemVersion))
                return;
            var deck = GetComponentDataFromEntity<Deck>(true)[dealer.Entity];
            var text = ui.Format;
            text = text.Replace("{Current}", deck.CurrentCount.ToString()).Replace("{Max}", deck.MaxCount.ToString());
            ui.Text.text = text;
        }).WithoutBurst().Run();
    }
}
