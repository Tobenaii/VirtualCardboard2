using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateBefore(typeof(PerformActionsSystem))]
public class TimedActionDeferralSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;
        Entities.ForEach((ref TimedDeferral deferral, ref PerformActions performer) =>
        {
            deferral.Timer += deltaTime;
            performer.NotReady = deferral.Timer < deferral.Delay;
        }).ScheduleParallel();
    }
}

