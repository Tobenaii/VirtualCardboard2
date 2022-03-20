using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct Damage : IComponentData
{
    public int Amount { get; set; }
}

public class DamageComponent : ComponentAuthoringBase
{
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, new Damage());
    }
}
