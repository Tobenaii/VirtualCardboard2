using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct ApplyModifiers : IPrefabCollection
{
    public Entity Entity { get; set; }
}

public class ApplyModifiersComponent : PrefabCollectionAuthoring<ApplyModifiers>
{
}
