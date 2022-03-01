using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(RequirementSystemGroup))]
public class StatusEffectRequirementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref PerformActions performer, in StatusEffectRequirement requirement) =>
        {
            Entity target;
            if (requirement.CheckOn == StatusEffectRequirement.On.Target)
                target = GetComponentDataFromEntity<Target>(true)[performer.Dealer].TargetEntity;
            else
                target = performer.Dealer;
            var statusBuffer = GetBufferFromEntity<StatusEffect>(true)[target];
            var status = statusBuffer[(int)requirement.Type];
            if (!status.Active)
            {
                performer.Status = IPerformActions.StatusType.Failed;
                performer.Failure = IPerformActions.FailureType.NotFound;
                return;
            }
        }).Schedule();
    }
}
