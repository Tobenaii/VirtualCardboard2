using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct Armour : IComponentData
{
    public float Value;
    public float BaseValue;
}

public class ArmourComponent : UnitComponentAuthoring<Armour>
{
    [SerializeField] private float _amount;

    protected override Armour AuthorComponent(World world)
    {
        return new Armour() { Value = _amount, BaseValue = _amount };
    }
}
