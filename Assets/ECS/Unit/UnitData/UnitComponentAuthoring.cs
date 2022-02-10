using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public abstract class UnitComponentAuthoring<T> : ComponentAuthoring<T> where T : struct, IComponentData
{
}
