using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct Damage : IComponentData
{
    public int Amount { get; set; }
}

public class DamageComponent : ComponentAuthoring<Damage>
{
    protected override Damage AuthorComponent(World world)
    {
        return new Damage();
    }
}
