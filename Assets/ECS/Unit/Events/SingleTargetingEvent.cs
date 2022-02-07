using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[assembly: RegisterGenericJobType(typeof(HealthEventSystem.GenericComponentEvent))]

[CreateAssetMenu(menuName = "VC2/Events/Single Target Event")]
public class SingleTargetingEvent : ComponentEvent<SingleTargeting, SingleTargetingEventSystem>
{
}

[DisableAutoCreation]
public class SingleTargetingEventSystem : ComponentEventSystem<SingleTargeting>
{

}
