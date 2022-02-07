using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct CardTester : IComponentData
{
    public Entity cardPrefab;
}

public class CardTesterComponent : UnitComponentAuthoring<CardTester>
{
    [SerializeField] private CardData _card;
    protected override CardTester AuthorComponent(World world)
    {
        return new CardTester() { cardPrefab = _card.GetPrefab(world.EntityManager) };
    }
}
