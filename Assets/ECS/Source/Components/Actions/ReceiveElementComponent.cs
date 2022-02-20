using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct ReceiveElement : IElementData, IBufferElementData
{
    public ElementType Type { get; set; }
    public int Count { get; set; }
}


public class ReceiveElementComponent : ElementSelectionComponent<ReceiveElement>
{
}
