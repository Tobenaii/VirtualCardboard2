using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(UISystemGroup))]
public class HealthTextUISystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((HealthTextUI ui, in Dealer dealer) =>
        {
            if (!EntityManager.GetChunk(dealer.Entity).DidChange(GetComponentTypeHandle<Health>(true), LastSystemVersion))
                return;
            var health = GetComponentDataFromEntity<Health>(true)[dealer.Entity];

            var text = ui.Format;
            ui.TextMesh.text = text.Replace("{Current}", health.CurrentValue.ToString()).Replace("{Max}", health.MaxValue.ToString());
        }).WithoutBurst().Run();
    }
}
