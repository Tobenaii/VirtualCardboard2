using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Events/ATB Pool Event")]
public class ATBPoolEvent : ComponentEvent<ATBPool, ATBPoolEventSystem, IStatPool>
{
}

[DisableAutoCreation]
public class ATBPoolEventSystem : ComponentEventSystem<ATBPool>
{
}