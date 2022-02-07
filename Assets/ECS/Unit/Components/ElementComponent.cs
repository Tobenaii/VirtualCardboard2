using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;


public enum ElementType { Fire, Ice, Wind, Lightning, Water, Earth, Dark, Light, Life, Prismatic }


[InternalBufferCapacity(12)] [System.Serializable]
public struct Element : IBufferElementData
{
    public ElementType Type;
    public int Count;
}

public class ElementComponent : UnitBufferComponentAuthoring<Element>
{
    [SerializeField] private List<Element> _element;

    protected override NativeArray<Element> AuthorComponent(World world)
    {
        var array = new NativeArray<Element>(_element.Count, Allocator.Temp);
        for (int i = 0; i < _element.Count; i++)
        {
            array[i] = _element[i];
        }
        return array;
    }
}
