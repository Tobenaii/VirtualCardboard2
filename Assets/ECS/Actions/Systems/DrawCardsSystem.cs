using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class DrawCardsSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _endSimulationEcbSystem;

    protected override void OnCreate()
    {
        _endSimulationEcbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();
        Entities.ForEach((int entityInQueryIndex, Entity entity, in DrawCards draw) => {
            var deck = GetBufferFromEntity<Deck>(false)[draw.Entity];
            var cardHand = GetBufferFromEntity<CardHand>(false)[draw.Entity];
            for (int i = 0; i < draw.Amount; i++)
            {
                cardHand.Add(new CardHand() { Entity = deck[i].Entity });
            }
            deck.RemoveRange(0, draw.Amount);
            ecb.DestroyEntity(entityInQueryIndex, entity);
        }).ScheduleParallel();
        _endSimulationEcbSystem.AddJobHandleForProducer(this.Dependency);
    }
}
