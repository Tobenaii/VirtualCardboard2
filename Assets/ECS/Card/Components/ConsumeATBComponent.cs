using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct ConsumeATB : IComponentData
{
    public int amount;
}

public class ConsumeATBComponent : ComponentAuthoring<ConsumeATB>
{
    [SerializeField] private int _amount;

    protected override ConsumeATB AuthorComponent(World world)
    {
        return new ConsumeATB() { amount = _amount };
    }
}