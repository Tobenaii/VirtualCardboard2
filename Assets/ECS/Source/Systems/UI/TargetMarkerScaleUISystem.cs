using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class TargetMarkerScaleUISystem : SystemBase
{
    protected override void OnUpdate()
    {
        var camera = Camera.main.transform;
        var deltaTime = Time.DeltaTime;
        Entities.ForEach((TargetMarkerScaleUI ui) =>
        {
            var transform = ui.Transform;

            var target = GetComponentDataFromEntity<TargetMarker>(true)[ui.Target];

            transform.localScale = ui.SmoothDamp(transform.localScale, target.Scale);
        }).WithoutBurst().Run();
    }
}
