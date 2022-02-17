using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class DamageSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.WithChangeFilter<Damage>().ForEach((ref Health health, ref Damage damage) =>
        {
            health.CurrentValue -= damage.Amount;
            damage.Amount = 0;
        }).ScheduleParallel();
    }
}
