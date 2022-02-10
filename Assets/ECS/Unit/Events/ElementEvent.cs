using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Events/Element Event")]
public class ElementEvent : BufferEvent<Element, ElementEventSystem, IElementData>
{
}

[DisableAutoCreation]
public class ElementEventSystem : BufferEventSystem<Element>
{
}