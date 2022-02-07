using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Events/Single Target Event")]
public class SingleTargetingEvent : ComponentEvent<SingleTargeting, SingleTargetingEventSystem, ITargeting>
{
}

[DisableAutoCreation]
public class SingleTargetingEventSystem : ComponentEventSystem<SingleTargeting>
{

}
