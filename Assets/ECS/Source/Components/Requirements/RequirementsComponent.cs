using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct Requirement : IComponentData
{
    public Entity Prefab { get; set; }
}

public struct RequirementStatus : IComponentData
{
    public bool Failed { get; set; }
}

public class RequirementsComponent : ComponentAuthoringBase
{
    [ListDrawerSettings(Expanded = true, ShowItemCount = false, HideAddButton = true)]
    [SerializeField] private List<ReadWriteComponent> _requirements;

    [Button]
    private void AddRequirement()
    {
        var picker = new ComponentPicker();
        picker.OpenAndGetInstance((instance) => _requirements.Add(instance));
    }

    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        var reqEntity = dstManager.CreateEntity();
        dstManager.SetName(reqEntity, "Action Requirement");
        dstManager.AddComponent<RequirementStatus>(reqEntity);
        dstManager.AddComponent<Prefab>(reqEntity);
        for (int i = 0; i < _requirements.Count; i++)
        {
            var action = _requirements[i];
            action.Component.AuthorComponent(reqEntity, dstManager);
        }
        dstManager.AddComponentData(entity, new Requirement() { Prefab = reqEntity });
    }
}
