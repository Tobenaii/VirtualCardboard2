using Unity.Entities;

[UpdateInGroup(typeof(RequirementSystemGroup))]
public class EmptyATBPoolRequirementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref PerformActions performer, in EmptyATBPoolRequirement requirement) =>
        {
            var atb = GetComponentDataFromEntity<ATBPool>(true)[performer.Dealer];
            if (atb.CurrentCount != 0)
            {
                performer.Status = IPerformActions.StatusType.Failed;
                performer.Failure = IPerformActions.FailureType.TooMany;
                performer.Message = "ATBPool";
            }
        }).ScheduleParallel();
    }
}