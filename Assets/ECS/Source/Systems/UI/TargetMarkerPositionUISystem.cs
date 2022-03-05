using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class TargetMarkerPositionUISystem : SystemBase
{
    protected override void OnUpdate()
    {
        var camera = Camera.main.transform;
        var deltaTime = Time.DeltaTime;
        Entities.ForEach((TargetMarkerPositionUI ui) =>
        {
            var transform = ui.Transform;

            var target = GetComponentDataFromEntity<TargetMarker>(true)[ui.Target];

            transform.position = ui.SmoothDamp(transform.position, target.Position);
            float prevZ = transform.eulerAngles.z;
            transform.LookAt(camera);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, prevZ);
            transform.Rotate(transform.forward, 20 * deltaTime, Space.World);
        }).WithoutBurst().Run();
    }
}
