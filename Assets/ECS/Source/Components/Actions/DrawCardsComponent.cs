using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct DrawCards : IComponentData
{
    public int Amount { get; set; }
}

public class DrawCardsComponent : ComponentAuthoring<DrawCards>
{
    [SerializeField] private int _amount;
    protected override DrawCards AuthorComponent(World world)
    {
        return new DrawCards() { Amount = _amount };
    }
}
