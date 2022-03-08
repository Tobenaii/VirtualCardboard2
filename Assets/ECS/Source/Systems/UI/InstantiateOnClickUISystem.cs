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
        Entities.ForEach((InstantiateOnClickUI ui) =>
        {
            if (ui.ClickEvent.HasClicked)
            {
                foreach (var action in ui.Entities)
                {
                    ecb.Instantiate(action);
                    ecb.AddComponent<Dealer>(action);
                    ecb.SetComponent<Dealer>(action, new Dealer() { Entity = ui.Dealer });
                }
            }
        }).WithoutBurst().Run();
    }
}
