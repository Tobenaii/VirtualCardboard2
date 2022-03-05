using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public interface IElementWheel
{
    public int Type { get; set; }
    public float Percentage { get; set; }
}

public struct ElementWheel : IElementWheel, IBufferElementData
{
    public int Type { get; set; }
    public float Percentage { get; set; }
}

public class ElementWheelComponent : BufferComponentAuthoring<ElementWheel>
{
    [SerializeField] private List<ElementData> _elements;
    protected override NativeArray<ElementWheel> AuthorComponent(World world)
    {
        var array = new NativeArray<ElementWheel>(_elements.Count, Allocator.Temp);

        int index = 0;
        foreach (var element in _elements)
        {
            array[index] = new ElementWheel() { Type = element.Index, Percentage = 100.0f / _elements.Count };
            index++;
        }
        return array;
    }
}
