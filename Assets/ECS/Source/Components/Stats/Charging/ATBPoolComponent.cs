using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct ATBPool : IStatPool, IComponentData
{
    public bool Enabled { get; set; }
    public int MaxCount { get; set; }
    public int CurrentCount { get; set; }
    public float ChargeTime { get; set; }
    public float ChargeTimer { get; set; }
}

public class ATBPoolComponent : StatPoolAuthoring<ATBPool>
{
}
