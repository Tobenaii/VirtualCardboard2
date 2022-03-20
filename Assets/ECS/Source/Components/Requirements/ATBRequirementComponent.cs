using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public struct ATBRequirement : IComponentData
{
    public int Amount { get; set; }
}

public class ATBRequirementComponent : ComponentAuthoringBase
{
    [SerializeField] private int _amount;

    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, new ATBRequirement() { Amount = _amount });
    }
}
