using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct DrawCards : IComponentData
{
    public Entity Entity;
    public int Amount;
}