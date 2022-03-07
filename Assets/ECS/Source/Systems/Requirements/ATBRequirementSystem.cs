using Unity.Entities;

[UpdateInGroup(typeof(RequirementSystemGroup))]
public class ATBRequirementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref RequirementStatus status, in Dealer dealer, in ATBRequirement requirement) =>
        {
            var atb = GetComponentDataFromEntity<ATB>(true)[dealer.Entity];
            if (atb.CurrentValue < requirement.Amount)
            {
                status.Failed = true;
            }
        }).ScheduleParallel();
    }
}
