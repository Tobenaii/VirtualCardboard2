using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class TargetSelectionSystem : SystemBase
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
        Entities.ForEach((ref Target target, ref TargetSelection switching) =>
        {
            if (targetableArray.Length == 0)
                return;
            if (tab)
            {
                switching.Index++;
                if (switching.Index >= targetableArray.Length)
                    switching.Index = 0;
            }
            var targetableEntity = targetableArray[switching.Index];
            target.TargetEntity = targetableEntity;
        }).WithDisposeOnCompletion(targetableArray).Schedule();
    }
}
