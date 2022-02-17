using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class ATBPoolSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;
        Entities.ForEach((ref ATB atb, ref ATBPool pool) =>
        {
            if (pool.CurrentCount == 0 || !pool.Enabled)
                return;
            pool.ChargeTimer += deltaTime;
            if (pool.ChargeTimer >= pool.ChargeTime)
            {
                pool.ChargeTimer = 0;
                pool.CurrentCount--;
                atb.CurrentValue++;
                atb.CurrentValue = math.min(atb.CurrentValue, atb.MaxValue);
            }
        }).ScheduleParallel();
    }
}
