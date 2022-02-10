using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct AllHandsDrawCards : IComponentData
{
    public bool HasDrawn;
    public int Amount;
}

public class AllHandsDrawCardsComponent : PhaseComponentAuthoring<AllHandsDrawCards>
{
    [SerializeField] private int _amount;
    protected override AllHandsDrawCards AuthorComponent(World world)
    {
        return new AllHandsDrawCards() { HasDrawn = false, Amount = _amount };
    }
}
