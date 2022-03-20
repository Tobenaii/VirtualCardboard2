using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct EmptyATBRequirement : IComponentData
{
}

public class EmptyATBRequirementComponent : ComponentAuthoringBase
{
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponent<EmptyATBRequirement>(entity);
    }
}