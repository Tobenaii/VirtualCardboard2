using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(UISystemGroup))]
public class ActionButtonUISystem : SystemBase
{
    private BeginInitializationEntityCommandBufferSystem _commandBuffer;

    protected override void OnCreate()
    {
        _commandBuffer = World.GetExistingSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _commandBuffer.CreateCommandBuffer();
        Entities.ForEach((ActionButtonUI ui, in Dealer dealer, in Action action) =>
        {
            if (ui.ClickEvent.HasClicked)
            {
                var actionInstance = ecb.Instantiate(action.Prefab);
                ecb.SetComponent<Dealer>(actionInstance, new Dealer() { Entity = dealer.Entity });
                return;
            }
        }).WithoutBurst().Run();
        _commandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}
