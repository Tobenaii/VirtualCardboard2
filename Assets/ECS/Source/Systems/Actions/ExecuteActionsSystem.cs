using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ExecuteActionsSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _commandBuffer;

    protected override void OnCreate()
    {
        _commandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _commandBuffer.CreateCommandBuffer().AsParallelWriter();
        Entities.ForEach((int entityInQueryIndex, ref DynamicBuffer<ExecuteAction> actions) =>
        {
            for (int i = 0; i < actions.Length; i++)
            {
                var action = actions[i];
                if (action.Executed)
                    continue;
                ecb.Instantiate(entityInQueryIndex, actions[i].Action);
                action.Executed = true;
                actions[i] = action;
            }
        }).ScheduleParallel();
        _commandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}
