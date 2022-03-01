using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct EmptyATBPoolRequirement : IComponentData
{
}

public class EmptyATBPoolRequirementComponent : ComponentAuthoring<EmptyATBPoolRequirement>
{
    protected override EmptyATBPoolRequirement AuthorComponent(World world)
    {
        return new EmptyATBPoolRequirement();
    }
}