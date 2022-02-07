using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct Health : IComponentData
{
    public float Value;
    public float MaxValue;
}

public class HealthComponent : UnitComponentAuthoring<Health>
{
    [SerializeField] private float _health;

    protected override Health AuthorComponent(World world)
    {
        return new Health() { Value = _health, MaxValue = _health };
    }
}
