using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class SingleTargetMarkerSystem : SystemBase
{

    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation position, ref NonUniformScale scale, ref Rotation rotation, in SingleTargetMarker marker) =>
        {
            position.Value = marker.position;
            scale.Value = marker.scale;
            rotation.Value = marker.rotation;
        }).ScheduleParallel();
    }
}

public class MarkerTargetToSmoothDampSystem : SystemBase
{
    private EntityQuery _targetQuery;

    protected override void OnCreate()
    {
        _targetQuery = GetEntityQuery(ComponentType.ReadOnly<SingleTarget>());
    }

    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;
        var targetArray = _targetQuery.ToEntityArray(Unity.Collections.Allocator.TempJob);
        var cameraRot = Camera.main.transform.rotation;
        Entities.ForEach((int entityInQueryIndex, ref SingleTargetMarker marker, ref SmoothDamp3 smoothDamp) =>
        {
            var singleTarget = GetComponentDataFromEntity<SingleTarget>(true)[targetArray[entityInQueryIndex]];
            if (singleTarget.isTargeting)
            {
                var target = singleTarget.target;
                var localToWorld = GetComponentDataFromEntity<LocalToWorld>(true)[target];
                var targetMarkerPoint = GetComponentDataFromEntity<Targetable>(true)[target];
                float3 newWorldPos = math.mul(localToWorld.Value, new float4(targetMarkerPoint.localMarkerOffset, 1)).xyz;
                var newPos = smoothDamp.SmoothDamp(marker.position, newWorldPos, ref marker.positionVelocity, deltaTime);
                marker.scale = smoothDamp.SmoothDamp(marker.scale, targetMarkerPoint.markerScale, ref marker.scaleVelocity, deltaTime);
                marker.position = newPos;
                marker.rotation = cameraRot;
            }
        }).WithDisposeOnCompletion(targetArray).WithoutBurst().Schedule();
    }
}
