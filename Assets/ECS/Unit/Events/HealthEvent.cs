using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[assembly: RegisterGenericJobType(typeof(HealthEventSystem.GenericComponentEvent))]

[CreateAssetMenu(menuName = "VC2/Events/Health Event")]
public class HealthEvent : ComponentEvent<Health, HealthEventSystem>
{
}

[DisableAutoCreation]
public class HealthEventSystem : ComponentEventSystem<Health>
{

}
