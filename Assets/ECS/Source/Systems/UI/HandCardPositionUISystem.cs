using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(UISystemGroup))]
public class HandCardPositionUISystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((int entityInQueryIndex, HandCardPositionUI ui) =>
        {
            var handCards = GetBufferFromEntity<HandCard>(true)[ui.Target];
            if (entityInQueryIndex >= handCards.Length)
            {
                ui.Transform.gameObject.SetActive(false);
                return;
            }
            else if (!ui.Transform.gameObject.activeSelf)
                ui.Transform.gameObject.SetActive(true);
            var baseOffsetCount = (handCards.Length - 1) / 2.0f;
            var cardIndex = entityInQueryIndex - baseOffsetCount;
            ui.Transform.SetSiblingIndex(entityInQueryIndex);
            var offset = new Vector2(Mathf.Sin(cardIndex * (Mathf.Deg2Rad * ui.CircularOffset)), Mathf.Cos(cardIndex * (Mathf.Deg2Rad * ui.CircularOffset))) * ui.Radius;
            var targetPos = offset;
            targetPos += Vector2.right * ui.HorizontalOffset * cardIndex;
            ui.Transform.pivot = ui.SmoothDamp(ui.Transform.pivot, targetPos);
            ui.Transform.rotation = Quaternion.identity;
            ui.Transform.Rotate(Vector3.forward, cardIndex * -ui.RotationalOffset);
        }).WithoutBurst().Run();
    }
}
