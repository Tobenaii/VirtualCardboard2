using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayCardSystem : SystemBase
{
    private BeginInitializationEntityCommandBufferSystem _endSimulationEcbSystem;

    protected override void OnCreate()
    {
        _endSimulationEcbSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();

        Entities.ForEach((int entityInQueryIndex, Entity entity, in PlayCard playCard, in ActionStatus status) =>
        {
            if (status.Status == IActionStatus.StatusType.Success)
            {
                var dealer = playCard.Dealer;
                var target = GetComponentDataFromEntity<Target>(true)[dealer];
                var card = ecb.Instantiate(entityInQueryIndex, playCard.Card);
                ecb.SetComponent<Target>(entityInQueryIndex, card, new Target() { TargetEntity = target.TargetEntity, Dealer = playCard.Dealer });
            }
            ecb.DestroyEntity(entityInQueryIndex, entity);
        }).ScheduleParallel();
        _endSimulationEcbSystem.AddJobHandleForProducer(this.Dependency);
    }
}
