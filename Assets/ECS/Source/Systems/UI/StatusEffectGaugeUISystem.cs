using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

[UpdateInGroup(typeof(UISystemGroup))]
public class StatusEffectGaugeUISystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((StatusEffectGaugeUI ui, in Dealer dealer) =>
        {
            if (!EntityManager.GetChunk(dealer.Entity).DidChange(GetBufferTypeHandle<StatusEffect>(true), LastSystemVersion))
                return;
            var elements = GetBufferFromEntity<StatusEffect>(true)[dealer.Entity];
            Transform group = ui.ResourceHolder;
            int resourceIndex = 0;
            foreach (var element in elements)
            {
                if (element.Active)
                {
                    group.GetChild(resourceIndex).gameObject.SetActive(true);
                    resourceIndex++;
                    //TODO: Cache the images;
                    group.GetChild(resourceIndex).GetComponent<Image>().sprite = ui.ElementData[element.Type].Icon;
                }
            }
            for (int i = resourceIndex; i < group.childCount; i++)
            {
                group.GetChild(i).gameObject.SetActive(false);
            }
        }).WithoutBurst().Run();
    }
}
