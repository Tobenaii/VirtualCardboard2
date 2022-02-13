using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Events/Phase Status Event")]
public class PhaseStatusEvent : ComponentEvent<PhaseNotification, PhaseStatusEventSystem, IStatusMessage>
{
}

[DisableAutoCreation]
public class PhaseStatusEventSystem : ComponentEventSystem<PhaseNotification>
{
}