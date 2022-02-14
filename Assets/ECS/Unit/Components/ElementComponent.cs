using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public interface IElementData : IBufferElementData
{
    public ElementType Type { get; set; }
    public int Count { get; set; }
}

public enum ElementType { Fire, Ice, Wind, Lightning, Water, Earth, Dark, Light, Life, Prismatic }


[InternalBufferCapacity(12)] [System.Serializable]
public struct Element : IElementData
{
    public ElementType Type { get; set; }
    public int Count { get; set; }
}

public class ElementComponent : BufferComponentAuthoring<Element>
{
    [System.Serializable]
    private struct ElementAuthoring
    {
        [field: SerializeField] public ElementType Type { get; private set; }
        [field: SerializeField] public int Count { get; private set; }

    }
    [SerializeField] private List<ElementAuthoring> _element;

    protected override NativeArray<Element> AuthorComponent(World world)
    {
        var array = new NativeArray<Element>(_element.Count, Allocator.Temp);
        for (int i = 0; i < _element.Count; i++)
        {
            var authoring = _element[i];
            array[i] = new Element() { Type = authoring.Type, Count = authoring.Count };
        }
        return array;
    }
}
