using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[InternalBufferCapacity(5)]
public struct Requirement : IComponentData
{
    public Entity Prefab { get; set; }
}

public class RequirementsComponent : ComponentAuthoring<Requirement>
{
    [ListDrawerSettings(Expanded = true, ShowItemCount = false, HideAddButton = true)]
    [SerializeField] private List<ReadWriteComponent> _actions;

    [Button]
    private void AddAction()
    {
        var picker = new ComponentPicker();
        picker.OpenAndGetInstance((instance) => _actions.Add(instance));
    }

    protected override Requirement AuthorComponent(World world)
    {
        var entity = world.EntityManager.CreateEntity();
        world.EntityManager.AddComponent<RequirementStatus>(entity);
        world.EntityManager.AddComponent<Prefab>(entity);
        for (int i = 0; i < _actions.Count; i++)
        {
            var action = _actions[i];
            action.Component.AuthorComponent(entity, world.EntityManager);
        }
        return new Requirement() { Prefab = entity };
    }
}
