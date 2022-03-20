using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct ResetATBPool : IComponentData { }

public class ResetATBPoolComponent : ComponentAuthoringBase
{
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponent<ResetATBPool>(entity);
    }

    public override void UpdateComponent(Entity entity, EntityManager dstManager)
    {
    }
}
