using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct Health : IStat
{
    public float BaseValue { get; set; }

    public float CurrentValue { get; set; }

    public float MaxValue { get; set; }
}

public class HealthComponent : UnitComponentAuthoring<Health>
{
    [SerializeField] private float _maxHealth;

    protected override Health AuthorComponent(World world)
    {
        return new Health() { BaseValue = _maxHealth, CurrentValue = _maxHealth, MaxValue = _maxHealth };
    }
}
