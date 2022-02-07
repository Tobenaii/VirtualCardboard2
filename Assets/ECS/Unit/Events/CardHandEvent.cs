using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Events/Card Hand Event")]
public class CardHandEvent : BufferEvent<CardHand, CardHandEventSystem, IEntityBuffer>
{
}

[DisableAutoCreation]
public class CardHandEventSystem : BufferEventSystem<CardHand>
{
}