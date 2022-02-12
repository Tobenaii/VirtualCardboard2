using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class ATBSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;
        Entities.ForEach((ref ATB atb) =>
        {
            if (atb.Pool == 0)
                return;
            atb.ChargeTimer += deltaTime;
            if (atb.ChargeTimer >= atb.ChargeTime)
            {
                atb.ChargeTimer = 0;
                atb.Pool--;
                atb.Charges++;
                atb.Charges = math.min(atb.Charges, atb.MaxCharges);
            }
        }).ScheduleParallel();
        this.CompleteDependency();
    }
}
