using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(UISystemGroup))]
public class InstantiateOnClickUISystem : SystemBase
{
    private BeginInitializationEntityCommandBufferSystem _commandBuffer;

    protected override void OnCreate()
    {
        _commandBuffer = World.GetExistingSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _commandBuffer.CreateCommandBuffer();
        Entities.ForEach((InstantiateOnClickUI ui, in Dealer dealer) =>
        {
            if (ui.ClickEvent.HasClicked)
            {
                var actionInstance = ecb.Instantiate(ui.Action);
                ecb.SetComponent<Dealer>(actionInstance, new Dealer() { Entity = dealer.Entity });
                return;
            }
        }).WithoutBurst().Run();
        _commandBuffer.AddJobHandleForProducer(this.Dependency);
    }
}
