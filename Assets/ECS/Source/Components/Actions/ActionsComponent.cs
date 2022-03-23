using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Serialization;

public struct Dealer : IComponentData
{
    public Entity Entity { get; set; }
}

public struct Action : IComponentData
{
    public Entity Prefab { get; set; }
}

[MovedFrom(true, sourceClassName: "RequirementActionComponent")]
public class ActionsComponent : ComponentAuthoringBase
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
        var actionEntity = dstManager.CreateEntity();
        dstManager.AddComponent<Dealer>(actionEntity);
        dstManager.AddComponent<Prefab>(actionEntity);
        for (int i = 0; i < _actions.Count; i++)
        {
            var action = _actions[i];
            action.Component.AuthorComponent(actionEntity, dstManager);
        }
        dstManager.AddComponentData(entity, new Action() { Prefab = actionEntity });
        dstManager.AddComponent<Dealer>(entity);
        dstManager.AddComponent<RequirementStatus>(entity);

    }
}
