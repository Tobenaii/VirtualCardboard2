
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct RequirementStatus : IComponentData
{
    public bool Failed { get; set; }
}

public class RequirementStatusComponent : ComponentAuthoring<RequirementStatus>
{
    protected override RequirementStatus AuthorComponent(World world)
    {
        return new RequirementStatus();
    }
}
