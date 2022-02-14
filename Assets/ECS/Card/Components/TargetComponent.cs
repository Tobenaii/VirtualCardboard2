using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct Target : IComponentData
{
    public Entity dealer;
    public Entity target;
}

public class TargetComponent : ComponentAuthoring<Target>
{
    protected override Target AuthorComponent(World world)
    {
        return new Target();
    }
}
