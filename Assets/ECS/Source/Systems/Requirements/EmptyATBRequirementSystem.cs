using Unity.Entities;

[UpdateInGroup(typeof(RequirementSystemGroup))]
public class EmptyATBRequirementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref PerformActions performer, in EmptyATBRequirement requirement) =>
        {
            var atb = GetComponentDataFromEntity<ATB>(true)[performer.Dealer];
            if (atb.CurrentValue != 0)
            {
                performer.Status = IPerformActions.StatusType.Failed;
                performer.Failure = IPerformActions.FailureType.TooMany;
                performer.Message = "ATB";
            }
        }).ScheduleParallel();
    }
}