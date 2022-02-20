using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public struct ATBRequirement : IComponentData
{
    public bool Consume { get; set; }
    public int Amount { get; set; }
}

public class ATBRequirementComponent : ComponentAuthoring<ATBRequirement>
{
    [SerializeField] private int _amount;
    [SerializeField] private bool _consume;

    protected override ATBRequirement AuthorComponent(World world)
    {
        return new ATBRequirement() { Amount = _amount, Consume = _consume };
    }
}
