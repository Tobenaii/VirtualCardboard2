using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class StartCombatSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _endSimulationEcbSystem;

    protected override void OnCreate()
    {
        _endSimulationEcbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();

        Entities.ForEach((int entityInQueryIndex, Entity entity, in StartCombat startCombat) =>
        {

        }).ScheduleParallel();
        _endSimulationEcbSystem.AddJobHandleForProducer(this.Dependency);
    }
}
