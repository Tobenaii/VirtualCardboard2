using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct DrawCards : IComponentData
{
    public int Amount { get; set; }
}

public class DrawCardsComponent : ComponentAuthoringBase
{
    [SerializeField] private int _amount;
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, new DrawCards() { Amount = _amount });
    }
}
