using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;


[CreateAssetMenu(menuName = "VC2/Events/Stat Event")]
public class StatEvent : BufferEvent<Stat, StatEventSystem, Stat.Type>
{
}

public class StatEventSystem : BufferEventSystem<Stat, Stat.Type> { }