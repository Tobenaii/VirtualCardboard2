using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

//THIS SYSTEM IS BAD, COME UP WITH A BETTER SOLUTION
public class UpdateATBSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _endSimulationEcbSystem;
    private EntityQuery _atbQuery;

    protected override void OnCreate()
    {
        _endSimulationEcbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        _atbQuery = this.GetEntityQuery(ComponentType.ReadWrite<ATB>());
    }

    protected override void OnUpdate()
    {
        var ecb = _endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();
        var entityArray = _atbQuery.ToEntityArray(Unity.Collections.Allocator.TempJob);

        Entities.ForEach((int entityInQueryIndex, in UpdateATB updateATB) =>
        {
            foreach (var entity in entityArray)
            {
                var atb = GetComponentDataFromEntity<ATB>(false)[entity];
                atb.CanCharge = true;
                ecb.SetComponent(entityInQueryIndex, entity, atb);
            }
        }).WithDisposeOnCompletion(entityArray).Schedule();
        _endSimulationEcbSystem.AddJobHandleForProducer(this.Dependency);
    }
}
