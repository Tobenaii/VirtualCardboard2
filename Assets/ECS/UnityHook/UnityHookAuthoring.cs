using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public abstract class UnityHookAuthoring<T> : ManagedComponentAuthoring<T> where T : class, IComponentData
{
}
