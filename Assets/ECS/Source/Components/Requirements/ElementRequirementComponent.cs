using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public struct ElementRequirement : IBufferElementData
{
    public ElementType Type { get; set; }
    public int Amount { get; set; }
}

public class ElementRequirementComponent : BufferComponentAuthoring<ElementRequirement>
{
    [System.Serializable]
    private struct Authoring
    {
        public ElementType Type;
        public int Amount;
    }

    [SerializeField] private List<Authoring> _requirements = new List<Authoring>();

    protected override NativeArray<ElementRequirement> AuthorComponent(World world)
    {
        var array = new NativeArray<ElementRequirement>(_requirements.Count, Allocator.Temp);
        int index = 0;
        foreach (var authoring in _requirements)
        {
            array[index] = new ElementRequirement() { Type = authoring.Type, Amount = authoring.Amount };
            index++;
        }
        return array;
    }
}
