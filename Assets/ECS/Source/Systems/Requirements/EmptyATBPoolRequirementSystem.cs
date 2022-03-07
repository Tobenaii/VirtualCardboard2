using Unity.Entities;

[UpdateInGroup(typeof(RequirementSystemGroup))]
public class EmptyATBPoolRequirementSystem : SystemBase
{

    protected override void OnUpdate()
    {
        Entities.ForEach((ref RequirementStatus status, in Dealer dealer, in EmptyATBPoolRequirement requirement) =>
        {
            var atb = GetComponentDataFromEntity<ATBPool>(true)[dealer.Entity];
            if (atb.CurrentCount != 0)
            {
                status.Failed = true;
            }
        }).ScheduleParallel();
    }
}