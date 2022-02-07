using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct Armour : IAttribute
{
    public float Value { get; set; }
}

public class ArmourComponent : AttributeAuthoring<Armour>
{
}
