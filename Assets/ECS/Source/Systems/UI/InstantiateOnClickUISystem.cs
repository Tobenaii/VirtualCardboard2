using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

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
                foreach (var action in ui.Entities)
                {
                    var actionInstance = ecb.Instantiate(action);
                    ecb.SetComponent<Dealer>(actionInstance, new Dealer() { Entity = dealer.Entity });
                }
            }
        }).WithoutBurst().Run();
    }
}
