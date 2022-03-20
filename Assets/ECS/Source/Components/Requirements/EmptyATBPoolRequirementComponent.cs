using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct EmptyATBPoolRequirement : IComponentData
{
}

public class EmptyATBPoolRequirementComponent : ComponentAuthoringBase
{
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponent<EmptyATBPoolRequirement>(entity);
    }
}