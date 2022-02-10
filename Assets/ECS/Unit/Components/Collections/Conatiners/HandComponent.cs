using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Hand : IPrefabCollectionContainer<HandCard>
{
    public int MaxCount { get; set; }
    public int CurrentCount { get; set; }
}

public class HandComponent : PrefabCollectionContainerAuthoring<Hand, HandCard, Card>
{
}
