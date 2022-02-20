using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Events/Element Wheel Event")]
public class ElementWheelEvent : BufferEvent<ElementWheel, ElementWheelEventSystem, IElementWheel>
{
}

[DisableAutoCreation]
public class ElementWheelEventSystem : BufferEventSystem<ElementWheel>
{
}
