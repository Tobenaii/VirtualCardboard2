using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateAfter(typeof(TargetSelectionSystem))]
public class TargetMarkerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref TargetMarker marker, in Target target) =>
        {
            if (!target.HasTarget)
                return;
            var localToWorld = GetComponentDataFromEntity<LocalToWorld>(true)[target.TargetEntity];
            var targetable = GetComponentDataFromEntity<Targetable>(true)[target.TargetEntity];
            float3 newWorldPos = math.mul(localToWorld.Value, new float4(targetable.Position, 1)).xyz;
            marker.Position = newWorldPos;
            marker.Scale = targetable.Scale;
        }).ScheduleParallel();
    }
}
