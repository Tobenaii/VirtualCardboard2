using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ATB : IChargeStat
{
    public int Charges { get; set; }
    public float ChargeTime { get; set; }
    public int MaxCharges { get; set; }
    public int MaxPool { get; set; }
    public int Pool { get; set; }
    public float ChargeTimer { get; set; }
}

public class ATBComponent : ChargeStatAuthoring<ATB>
{
}
