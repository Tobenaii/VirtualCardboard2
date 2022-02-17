using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public struct ExecuteAction : IBufferElementData
{
    //This is bad idk
    public bool Executed { get; set; }
    public Entity Action { get; set; }
}

public class ExecuteActionsComponent : BufferComponentAuthoring<ExecuteAction>
{
    [SerializeField] private List<ModEntity> _actions = new List<ModEntity>();
    protected override NativeArray<ExecuteAction> AuthorComponent(World world)
    {
        var array = new NativeArray<ExecuteAction>(_actions.Count, Allocator.Temp);
        int index = 0;
        foreach (var action in _actions)
        {
            array[index] = new ExecuteAction() { Action = action.GetPrefab(world.EntityManager) };
            index++;
        }
        return array;
    }
}
