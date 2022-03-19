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

[MovedFrom(true, sourceClassName: "ActionsComponent")]
public class ActionComponent : ComponentAuthoring<Action>
{
    [ListDrawerSettings(Expanded = true, ShowItemCount = false, HideAddButton = true)]
    [SerializeField] private List<ReadWriteComponent> _actions;

    [Button]
    private void AddAction()
    {
        var picker = new ComponentPicker();
        picker.OpenAndGetInstance((instance) => _actions.Add(instance));
    }

    public override void AuthorDependencies(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponent<Dealer>(entity);
    }

    protected override Action AuthorComponent(World world)
    {
        var entity = world.EntityManager.CreateEntity();
        world.EntityManager.AddComponent<Dealer>(entity);
        world.EntityManager.AddComponent<Prefab>(entity);
        for (int i = 0; i < _actions.Count; i++)
        {
            var action = _actions[i];
            action.Component.AuthorComponent(entity, world.EntityManager);
        }
        return new Action() { Prefab = entity };
    }
}
