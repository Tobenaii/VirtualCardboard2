using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Deck : ICollectionContainer
{
    public int MaxCount { get; set; }
    public int CurrentCount { get; set; }
}

public class DeckComponent : CollectionContainerAuthoring<Deck, DeckCard>
{
}
