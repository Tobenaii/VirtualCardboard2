using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(UISystemGroup))]
public class ATBStoreUISystem : SystemBase
{
    protected override void OnUpdate()
    {
        //TODO: Separate this into ATBPoolUI / ATBChargeUI
        Entities.ForEach((ATBStoreUI ui, in Dealer dealer) =>
        {
            if (!EntityManager.GetChunk(dealer.Entity).DidChange(GetComponentTypeHandle<ATBPool>(true), LastSystemVersion))
                return;
            var pool = GetComponentDataFromEntity<ATBPool>(true)[dealer.Entity];
            var atb = GetComponentDataFromEntity<ATB>(true)[dealer.Entity];

            ui.PoolText.text = ui.PoolTextFormat.Replace("{Current}", pool.CurrentCount.ToString()).Replace("{Max}", pool.MaxCount.ToString());
            ui.PoolImage.fillAmount = pool.ChargeTimer / pool.ChargeTime;
            for (int i = 0; i < atb.CurrentValue; i++)
            {
                ui.BarImages[i].fillAmount = 1;
            }
            for (int i = atb.CurrentValue; i < atb.MaxValue; i++)
            {
                ui.BarImages[i].fillAmount = 0;
            }
        }).WithoutBurst().Run();
    }
}
