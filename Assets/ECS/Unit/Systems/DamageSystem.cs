using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(ActionResolverGroup))]
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
