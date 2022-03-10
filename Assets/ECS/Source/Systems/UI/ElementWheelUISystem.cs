using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(UISystemGroup))]
public class ElementWheelUISystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ElementWheelUI ui, in Dealer dealer) =>
        {
            var elements = GetBufferFromEntity<ElementWheel>(true)[dealer.Entity];
            var start = 0.0f;
            for (int i = 0; i < elements.Length; i++)
            {
                var damperooski = ui.SmoothDamperooskis[i];
                var image = ui.Images[i];
                var element = elements[i];
                var target = start + element.Percentage / 100.0f;
                start += target;
                image.fillAmount = damperooski.SmoothDamp(image.fillAmount, target);
                image.color = ui.ElementData[element.Type].Color;
            }
        }).WithoutBurst().Run();
    }
}
