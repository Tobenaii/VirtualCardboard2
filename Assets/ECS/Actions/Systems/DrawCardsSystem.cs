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
            var deck = GetBufferFromEntity<DeckCard>(false)[draw.Entity];
            var cardHand = GetBufferFromEntity<HandCard>(false)[draw.Entity];
            var drawAmount = draw.Amount;
            if (deck.Length < drawAmount)
                drawAmount = deck.Length;
            for (int i = 0; i < drawAmount; i++)
            {
                cardHand.Add(new HandCard() { Entity = deck[i].Entity });
            }
            if (drawAmount != 0)
                deck.RemoveRange(0, drawAmount);
            ecb.DestroyEntity(entityInQueryIndex, entity);
        }).Schedule();
        _endSimulationEcbSystem.AddJobHandleForProducer(this.Dependency);
        CompleteDependency();
    }
}
