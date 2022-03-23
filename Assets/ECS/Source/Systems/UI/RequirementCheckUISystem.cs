using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(UISystemGroup))]
public class ButtonRequirementUISystem : SystemBase
{
    private BeginInitializationEntityCommandBufferSystem _commandBuffer;

    protected override void OnCreate()
    {
        _commandBuffer = World.GetExistingSystem<BeginInitializationEntityCommandBufferSystem>();
    }
    protected override void OnUpdate()
    {
        var ecb = _commandBuffer.CreateCommandBuffer();
        Entities.WithStructuralChanges().ForEach((RequirementCheckUI ui, RequirementStatus status) =>
        {
            ui.BlockingGroup.interactable = !status.Failed;
            ui.BlockingGroup.blocksRaycasts = !status.Failed;
        }).WithoutBurst().Run();
    }
}
