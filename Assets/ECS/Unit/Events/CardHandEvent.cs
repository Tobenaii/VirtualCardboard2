using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[assembly: RegisterGenericJobType(typeof(HealthEventSystem.GenericComponentEvent))]

[CreateAssetMenu(menuName = "VC2/Events/Card Hand Event")]
public class CardHandEvent : BufferComponentEvent<CardHand, CardHandEventSystem>
{
}

[DisableAutoCreation]
public class CardHandEventSystem : BufferComponentEventSystem<CardHand>
{

}