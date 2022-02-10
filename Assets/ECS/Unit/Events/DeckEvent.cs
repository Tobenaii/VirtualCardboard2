using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Events/Deck Event")]
public class DeckEvent : ComponentEvent<Deck, DeckEventSystem, ICollectionContainer>
{
}

[DisableAutoCreation]
public class DeckEventSystem : ComponentEventSystem<Deck>
{

}
