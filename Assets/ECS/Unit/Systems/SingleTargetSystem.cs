using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class SingleTargetSystem : SystemBase
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
        Entities.ForEach((ref SingleTarget target) =>
        {
            if (tab)
            {
                target.targetIndex++;
                if (target.targetIndex >= targetableArray.Length)
                    target.targetIndex = 0;
            }
            var targetable = targetableArray[target.targetIndex];
            target.target = targetable;
        }).WithDisposeOnCompletion(targetableArray).Schedule();
    }
}
