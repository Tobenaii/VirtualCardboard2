using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(UISystemGroup))]
public class RequirementCheckUISystem : SystemBase
{
    private BeginInitializationEntityCommandBufferSystem _commandBuffer;

    protected override void OnCreate()
    {
        _commandBuffer = World.GetExistingSystem<BeginInitializationEntityCommandBufferSystem>();
    }
    protected override void OnUpdate()
    {
        var ecb = _commandBuffer.CreateCommandBuffer();
        Entities.WithStructuralChanges().ForEach((RequirementCheckUI ui, in Dealer dealer) =>
        {
            if (!EntityManager.Exists(ui.Requirement))
            {
                ui.Requirement = EntityManager.Instantiate(GetComponentDataFromEntity<Requirement>(true)[ui.Prefab].Prefab);
                EntityManager.AddComponentData(ui.Requirement, dealer);
            }
            var requirementStatus = GetComponentDataFromEntity<RequirementStatus>(true)[ui.Requirement];
            ui.BlockingGroup.interactable = !requirementStatus.Failed;
            ui.BlockingGroup.blocksRaycasts = !requirementStatus.Failed;
        }).WithoutBurst().Run();
    }
}
