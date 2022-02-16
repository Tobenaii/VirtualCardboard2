using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[UpdateInGroup(typeof(ActionResolverGroup))]
public class StatModifierSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref DynamicBuffer<Stat> stats, ref DynamicBuffer<StatModifier> modifiers) =>
        {
            foreach (var modifier in modifiers)
            {
                float sign = math.sign((int)modifier.ModType);
                int index = math.abs((int)modifier.ModType);
                float amount = modifier.Amount;
                var stat = stats[index];
                stat.CurrentValue += amount * sign;
                stats[index] = stat;
            }
        }).Schedule();
    }
}
