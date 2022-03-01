using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct EmptyATBRequirement : IComponentData
{
}

public class EmptyATBRequirementComponent : ComponentAuthoring<EmptyATBRequirement>
{
    protected override EmptyATBRequirement AuthorComponent(World world)
    {
        return new EmptyATBRequirement();
    }
}