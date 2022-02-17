using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct ToggleATBPool : IComponentData
{
    public bool Enable { get; set; }
}

public class ToggleATBPoolComponent : ComponentAuthoring<ToggleATBPool>
{
    [SerializeField] private bool _enable;
    protected override ToggleATBPool AuthorComponent(World world)
    {
        return new ToggleATBPool() { Enable = _enable };
    }
}
