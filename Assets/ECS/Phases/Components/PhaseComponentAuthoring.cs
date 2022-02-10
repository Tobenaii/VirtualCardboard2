using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public abstract class PhaseComponentAuthoring<T> : ComponentAuthoring<T> where T : struct, IComponentData
{
}
