using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

[UpdateInGroup(typeof(UISystemGroup))]
public class ElementGaugeUISystem : SystemBase
{
    protected override void OnUpdate()
    {
        //TODO: Make ElementGaugeUI a buffer with an element for each element type
        Entities.ForEach((ElementGaugeUI ui, in Dealer dealer) =>
        {
            if (!EntityManager.GetChunk(dealer.Entity).DidChange(GetBufferTypeHandle<Element>(true), LastSystemVersion))
                return;
            var elements = GetBufferFromEntity<Element>(true)[dealer.Entity];
            Transform group = ui.ResourceHolder;
            int resourceIndex = 0;
            foreach (var element in elements)
            {
                for (int i = 0; i < element.Count; i++)
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
