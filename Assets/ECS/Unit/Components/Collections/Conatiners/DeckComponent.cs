using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[assembly: RegisterGenericJobType(typeof(DeckCollectionSystem.GenericContainerJob))]

public struct Deck : IPrefabCollectionContainer<DeckCard>
{
    public int MaxCount { get; set; }
    public int CurrentCount { get; set; }
}

public class DeckComponent : PrefabCollectionContainerAuthoring<Deck, DeckCard>
{
}

public class DeckCollectionSystem : CollectionContainerSystem<Deck, DeckCard> { }
