using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public struct ElementRequirement : IElementData, IBufferElementData
{
    public int Type { get; set; }
    public int Count { get; set; }
}

public class ElementRequirementComponent : ElementSelectionComponent<ElementRequirement>
{
}
