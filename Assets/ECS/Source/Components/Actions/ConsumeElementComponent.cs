using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct ConsumeElement : IElementData, IBufferElementData
{
    public ElementType Type { get; set; }
    public int Count { get; set; }
}

public class ConsumeElementComponent : ElementSelectionComponent<ConsumeElement>
{
}
