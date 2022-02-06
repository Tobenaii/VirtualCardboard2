using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class CardTesterSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _endSimulationEcbSystem;

    protected override void OnCreate()
    {
        _endSimulationEcbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var wasSpacePressed = Input.GetKeyDown(KeyCode.Space);
        var ecb = _endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();

        Entities.ForEach((Entity entity, int entityInQueryIndex, in SingleTarget target, in CardTester cardTester) =>
        {
            if (wasSpacePressed)
            {
                var cardEntity = ecb.Instantiate(entityInQueryIndex, cardTester.cardPrefab);
                ecb.SetComponent<Target>(entityInQueryIndex, cardEntity, new Target() { dealer = entity, target = target.target });
            }
        }).ScheduleParallel();

        _endSimulationEcbSystem.AddJobHandleForProducer(this.Dependency);
    }
}
