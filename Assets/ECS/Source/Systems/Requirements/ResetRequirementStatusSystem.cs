using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public class ResetRequirementStatusSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref RequirementStatus status) =>
        {
            status.Failed = false;
        }).ScheduleParallel();
    }
}
