using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct DealDamage : IComponentData
{
    public float amount;
}

public class DealDamageComponent : CardEffectAuthoring<DealDamage>
{
    [SerializeField] private float _damageAmount;
    protected override DealDamage AuthorComponent(World world)
    {
        return new DealDamage() { amount = _damageAmount };
    }
}
