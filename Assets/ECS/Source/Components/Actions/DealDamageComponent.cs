using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct DealDamage : IComponentData
{
    public int Amount;
}

public class DealDamageComponent : ComponentAuthoring<DealDamage>
{
    [SerializeField] private int _amount;
    protected override DealDamage AuthorComponent(World world)
    {
        return new DealDamage() { Amount = _amount };
    }
}
