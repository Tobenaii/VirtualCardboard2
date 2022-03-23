using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

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
        for (int i = 0; i < _requirements.Count; i++)
        {
            var action = _requirements[i];
            action.Component.AuthorComponent(entity, dstManager);
        }
        dstManager.AddComponent<RequirementStatus>(entity);
        dstManager.AddComponent<Dealer>(entity);
    }
}
