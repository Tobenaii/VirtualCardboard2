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
        Entities.ForEach((ATBStoreUI ui) =>
        {
            if (!EntityManager.GetChunk(ui.Target).DidChange(GetComponentTypeHandle<ATBPool>(true), LastSystemVersion))
                return;
            var pool = GetComponentDataFromEntity<ATBPool>(true)[ui.Target];
            var atb = GetComponentDataFromEntity<ATB>(true)[ui.Target];

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
