using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Events/Target Marker Event")]
public class TargetMarkerEvent : ComponentEvent<TargetMarker, TargetMarkerEventSystem, ITargetMarker>
{
}

[DisableAutoCreation]
public class TargetMarkerEventSystem : ComponentEventSystem<TargetMarker>
{

}
