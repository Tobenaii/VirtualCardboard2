using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public abstract class CardEffectAuthoring<T> : ComponentAuthoring<T> where T : struct, IComponentData
{
}
