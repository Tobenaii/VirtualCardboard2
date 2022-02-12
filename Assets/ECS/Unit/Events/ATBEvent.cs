using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Events/ATB Event")]
public class ATBEvent : ComponentEvent<ATB, ATBEventSystem, IChargeStat>
{
}

[DisableAutoCreation]
public class ATBEventSystem : ComponentEventSystem<ATB>
{
}