using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(RequirementSystemGroup))]
public class StatusEffectRequirementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref RequirementStatus status, in Dealer dealer, in TargetStatusEffectRequirement requirement) =>
        {
            Entity target = GetComponentDataFromEntity<Target>(true)[dealer.Entity].Entity;
            var statusBuffer = GetBufferFromEntity<StatusEffect>(true)[target];
            var effect = statusBuffer[(int)requirement.Type];
            if (!effect.Active)
            {
                status.Failed = true;
            }
        }).Schedule();
    }
}
