using Sirenix.OdinInspector;
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
    [System.Serializable]
    private struct DamageBuff
    {
        public ElementalDamageBuff Buff;
        public int Amount;
    }
    [SerializeField] private int _amount;
    [SerializeField] private List<DamageBuff> _buffs;
    protected override DealDamage AuthorComponent(World world)
    {
        return new DealDamage() { Amount = _amount };
    }
}
