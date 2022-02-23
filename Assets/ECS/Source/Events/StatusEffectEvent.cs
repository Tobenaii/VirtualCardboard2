using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Events/Status Effect Event")]
public class StatusEffectEvent : BufferEvent<StatusEffect, StatusEffectEventSystem, IStatusEffectData>
{
}

[DisableAutoCreation]
public class StatusEffectEventSystem : BufferEventSystem<StatusEffect>
{
}