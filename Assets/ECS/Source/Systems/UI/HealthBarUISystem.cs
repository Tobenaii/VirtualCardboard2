using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(UISystemGroup))]
public class HealthBarUISystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((HealthBarUI ui, in Dealer dealer) =>
        {
            var health = GetComponentDataFromEntity<Health>(true)[dealer.Entity];
            
            var target = (float)health.CurrentValue / health.MaxValue;
            var bar = ui.HealthBar;
            bar.fillAmount = ui.SmoothDamp(bar.fillAmount, target);
        }).WithoutBurst().Run();
    }
}
