using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct CardTester : IComponentAuthoring<CardTester, CardTesterComponent>
{
    public Entity cardPrefab;
}

public class CardTesterComponent : UnitComponentAuthoring<CardTester>
{
    [SerializeField] private Card _card;
    protected override CardTester AuthorComponent(World world)
    {
        return new CardTester() { cardPrefab = _card.CreatePrefab(world.EntityManager) };
    }
}
