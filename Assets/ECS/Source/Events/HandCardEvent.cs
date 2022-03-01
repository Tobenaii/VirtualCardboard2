using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Events/Card Hand Event")]
public class HandCardEvent : BufferEvent<HandCard, HandCardEventSystem, IPrefabCollection>
{
}

[DisableAutoCreation]
public class HandCardEventSystem : BufferEventSystem<HandCard>
{
}