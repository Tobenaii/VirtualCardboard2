using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Events/Phase Notification Event")]
public class PhaseNotificationEvent : ComponentEvent<PhaseNotification, PhaseNotificationEventSystem, INotification>
{
}

[DisableAutoCreation]
public class PhaseNotificationEventSystem : ComponentEventSystem<PhaseNotification>
{
}