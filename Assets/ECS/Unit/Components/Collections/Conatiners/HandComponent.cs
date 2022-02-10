using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Hand : ICollectionContainer
{
    public int MaxCount { get; set; }
    public int CurrentCount { get; set; }
}

public class HandComponent : CollectionContainerAuthoring<Hand, HandCard>
{
}
