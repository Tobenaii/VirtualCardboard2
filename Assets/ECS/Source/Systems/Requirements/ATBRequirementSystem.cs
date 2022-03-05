using Unity.Entities;

[UpdateInGroup(typeof(RequirementSystemGroup))]
public class ATBRequirementSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _commandBuffer;

    protected override void OnCreate()
    {
        _commandBuffer = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _commandBuffer.CreateCommandBuffer().AsParallelWriter();
        Entities.ForEach((int entityInQueryIndex, Entity entity, in Dealer dealer, in ATBRequirement requirement) =>
        {
            var atb = GetComponentDataFromEntity<ATB>(true)[dealer.Entity];
            if (atb.CurrentValue < requirement.Amount)
            {
                ecb.DestroyEntity(entityInQueryIndex, entity);
            }
        }).ScheduleParallel();
        _commandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}
