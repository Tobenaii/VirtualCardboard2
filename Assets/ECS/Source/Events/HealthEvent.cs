using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Events/Health Event")]
public class HealthEvent : ComponentEvent<Health, HealthEventSystem, IStat>
{
}

[DisableAutoCreation]
public class HealthEventSystem : ComponentEventSystem<Health>
{

}
