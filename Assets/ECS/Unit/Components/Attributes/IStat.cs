using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public interface IStat : IComponentData
{
    public float BaseValue { get; set; }
    public float CurrentValue { get; set; }
    public float MaxValue { get; set; }
}
