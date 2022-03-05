using Unity.Entities;

[UpdateInGroup(typeof(RequirementSystemGroup))]
public class EmptyATBPoolRequirementSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _commandBuffer;

    protected override void OnCreate()
    {
        _commandBuffer = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _commandBuffer.CreateCommandBuffer().AsParallelWriter();
        Entities.ForEach((int entityInQueryIndex, Entity entity, in Dealer dealer, in EmptyATBPoolRequirement requirement) =>
        {
            var atb = GetComponentDataFromEntity<ATBPool>(true)[dealer.Entity];
            if (atb.CurrentCount != 0)
            {
                ecb.DestroyEntity(entityInQueryIndex, entity);
            }
        }).ScheduleParallel();
        _commandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}