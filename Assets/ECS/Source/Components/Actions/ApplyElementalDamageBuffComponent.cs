using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public enum ElementalDamageBuff { Burn }

public struct ApplyElementalDamageBuff : IComponentData
{
    public ElementalDamageBuff DamgeBuff { get; set; }
}

public class ApplyElementalDamageBuffComponent : ComponentAuthoring<ApplyElementalDamageBuff>
{
    [SerializeField] private ElementalDamageBuff _buff;

    protected override ApplyElementalDamageBuff AuthorComponent(World world)
    {
        return new ApplyElementalDamageBuff() { DamgeBuff = _buff };
    }
}
