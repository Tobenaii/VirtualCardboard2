using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

public class AllHandsDrawCardsSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _endSimulationEcbSystem;
    private EntityQuery _deckHandQuery;

    protected override void OnCreate()
    {
        _endSimulationEcbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        _deckHandQuery = this.GetEntityQuery(ComponentType.ReadWrite<Hand>(), ComponentType.ReadWrite<Deck>());
    }

    protected override void OnUpdate()
    {
        var ecb = _endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();
        var entityArray = _deckHandQuery.ToEntityArray(Unity.Collections.Allocator.TempJob);
        Entities.ForEach((int entityInQueryIndex, ref AllHandsDrawCards draw) =>
        {
            if (draw.HasDrawn)
                return;
            foreach (var entity in entityArray)
            {
                var drawCards = new DrawCards() { Entity = entity, Amount = draw.Amount };
                var drawEntity = ecb.CreateEntity(entityInQueryIndex);
                ecb.AddComponent(entityInQueryIndex, drawEntity, drawCards);
            }
            draw.HasDrawn = true;
        }).WithDisposeOnCompletion(entityArray).ScheduleParallel();
        _endSimulationEcbSystem.AddJobHandleForProducer(this.Dependency);
    }
}
