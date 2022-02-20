using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Events/Action Event")]
public class ActionStatusEvent : ComponentEvent<PerformActions, ActionStatusEventSystem, IPerformActions>
{
}

[DisableAutoCreation]
public class ActionStatusEventSystem : ComponentEventSystem<PerformActions>
{
}