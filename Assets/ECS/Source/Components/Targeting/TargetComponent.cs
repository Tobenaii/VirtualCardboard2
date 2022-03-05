using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct Target : IComponentData
{
    public Entity Entity { get; set; }
}

public class TargetComponent : ComponentAuthoring<Target>
{

    protected override Target AuthorComponent(World world)
    {
        return new Target();
    }
}
