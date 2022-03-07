using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(RequirementSystemGroup))]
public class EmptyATBRequirementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref RequirementStatus status, in Dealer dealer, in EmptyATBRequirement requirement) =>
        {
            var atb = GetComponentDataFromEntity<ATB>(true)[dealer.Entity];
            if (atb.CurrentValue != 0)
            {
                status.Failed = true;
            }
        }).ScheduleParallel();
    }
}