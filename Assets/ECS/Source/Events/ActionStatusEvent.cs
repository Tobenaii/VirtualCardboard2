using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Events/Action Status Event")]
public class ActionStatusEvent : ComponentEvent<ActionStatus, ActionStatusEventSystem, IActionStatus>
{
}

[DisableAutoCreation]
public class ActionStatusEventSystem : ComponentEventSystem<ActionStatus>
{
}