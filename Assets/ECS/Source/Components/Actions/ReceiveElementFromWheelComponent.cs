using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct ReceiveElementFromWheel : IComponentData
{
}

public class ReceiveElementFromWheelComponent : ComponentAuthoringBase
{
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponent<ReceiveElementFromWheel>(entity);
    }

    public override void UpdateComponent(Entity entity, EntityManager dstManager)
    {
    }
}
