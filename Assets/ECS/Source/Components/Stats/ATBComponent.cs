using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct ATB : IStat, IComponentData
{
    public int BaseValue { get; set; }
    public int CurrentValue { get; set; }
    public int MaxValue { get; set; }
}

public class ATBComponent : StatAuthoring<ATB>
{
}
