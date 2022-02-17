using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[assembly: RegisterGenericJobType(typeof(DeckCollectionSystem.GenericContainerJob))]
public class DeckCollectionSystem : CollectionContainerSystem<Deck, DeckCards> { }