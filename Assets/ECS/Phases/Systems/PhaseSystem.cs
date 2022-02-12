using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PhaseSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _endSimulationEcbSystem;

    protected override void OnCreate()
    {
        _endSimulationEcbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();
        var deltaTime = Time.DeltaTime;
        Entities.ForEach((int entityInQueryIndex, Entity entity, ref PhaseData phase) =>
        {
            phase.Timer += deltaTime;
            if (phase.Timer >= phase.Time)
            {
                if (phase.HasNextPhase)
                    ecb.Instantiate(entityInQueryIndex, phase.NextPhase);

                ecb.DestroyEntity(entityInQueryIndex, entity);
            }
        }).Schedule();
        _endSimulationEcbSystem.AddJobHandleForProducer(this.Dependency);

    }
}
