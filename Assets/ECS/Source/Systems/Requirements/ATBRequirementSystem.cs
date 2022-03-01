using Unity.Entities;

[UpdateInGroup(typeof(RequirementSystemGroup))]
public class ATBRequirementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref PerformActions performer, in ATBRequirement requirement) =>
        {
            var atb = GetComponentDataFromEntity<ATB>(true)[performer.Dealer];
            if (atb.CurrentValue < requirement.Amount)
            {
                performer.Status = IPerformActions.StatusType.Failed;
                performer.Failure = IPerformActions.FailureType.NotEnough;
                performer.Message = "ATB";
            }
        }).ScheduleParallel();
    }
}
