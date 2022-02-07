using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class SingleTargetingSystem : SystemBase
{
    private EntityQuery _targetableQuery;
    protected override void OnCreate()
    {
        _targetableQuery = GetEntityQuery(ComponentType.ReadOnly<Targetable>());
        base.OnCreate();
    }
    protected override void OnUpdate()
    {
        var tab = Input.GetKeyDown(KeyCode.Tab);
        var targetableArray = _targetableQuery.ToEntityArray(Unity.Collections.Allocator.TempJob);
        Entities.ForEach((ref SingleTargeting target) =>
        {
            if (tab)
            {
                target.targetIndex++;
                if (target.targetIndex >= targetableArray.Length)
                    target.targetIndex = 0;
            }
            var targetableEntity = targetableArray[target.targetIndex];

            var localToWorld = GetComponentDataFromEntity<LocalToWorld>(true)[targetableEntity];
            var targetable = GetComponentDataFromEntity<Targetable>(true)[targetableEntity];
            float3 newWorldPos = math.mul(localToWorld.Value, new float4(targetable.offset, 1)).xyz;

            target.target = targetableEntity;
            target.Position = newWorldPos;
            target.Scale = targetable.scale;
        }).WithDisposeOnCompletion(targetableArray).Schedule();
        CompleteDependency();
    }
}
