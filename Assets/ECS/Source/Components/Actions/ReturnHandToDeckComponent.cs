using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct ReturnHandToDeck : IComponentData
{
}

public class ReturnHandToDeckComponent : ComponentAuthoringBase
{
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponent<ReturnHandToDeck>(entity);
    }

    public override void UpdateComponent(Entity entity, EntityManager dstManager)
    {
    }
}
