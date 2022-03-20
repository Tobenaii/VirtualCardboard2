using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct ConsumeATB : IComponentData
{
    public int Amount { get; set; }
}

public class ConsumeATBComponent : ComponentAuthoringBase
{
    [SerializeField] private int _amount;
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, new ConsumeATB() { Amount = _amount });
    }
}
