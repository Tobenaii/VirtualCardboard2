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
    [SerializeField] private List<ReadWriteComponent> _actions;

    [Button]
    private void AddAction()
    {
        var picker = new ComponentPicker();
        picker.OpenAndGetInstance((instance) => _actions.Add(instance));
    }

    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        var reqEntity = dstManager.CreateEntity();
        dstManager.AddComponent<RequirementStatus>(reqEntity);
        dstManager.AddComponent<Prefab>(reqEntity);
        for (int i = 0; i < _actions.Count; i++)
        {
            var action = _actions[i];
            action.Component.AuthorComponent(reqEntity, dstManager);
        }
        dstManager.AddComponentData(entity, new Requirement() { Prefab = entity });
    }
}
