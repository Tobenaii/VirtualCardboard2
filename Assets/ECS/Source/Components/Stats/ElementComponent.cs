using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public interface IElementData : IBufferElementData
{
    public ElementType Type { get; set; }
    public int Count { get; set; }
}

public enum ElementType { Fire, Ice, Wind, Lightning, Water, Earth, Dark, Light, Life, Prismatic }


[InternalBufferCapacity(10)] [System.Serializable]
public struct Element : IElementData
{
    public ElementType Type { get; set; }
    public int Count { get; set; }
    public FixedString32 Name { get; set; }
}

public class ElementComponent : BufferComponentAuthoring<Element>
{
    [System.Serializable]
    private class ElementAuthoring
    {
        [field: SerializeField] public ElementType Type { get; private set; }
        [field: SerializeField] public int Count { get; private set; }

    }
    [SerializeField] private List<ElementAuthoring> _elements;

    protected override NativeArray<Element> AuthorComponent(World world)
    {
        var array = new NativeArray<Element>(Enum.GetNames(typeof(ElementType)).Length, Allocator.Temp);
        int index = 0;
        for (int i = 0; i < array.Length; i++)
        {
            var authoring = _elements.Where(x => x.Type == (ElementType)i).FirstOrDefault();
            if (authoring != null)
            {
                array[i] = new Element()
                {
                    Type = authoring.Type,
                    Count = authoring.Count,
                    Name = authoring.Type.ToString()
                };
                index++;
            }
            else
                array[i] = new Element() { Type = (ElementType)i, Name = ((ElementType)i).ToString() };
        }
        return array;
    }
}

public abstract class ElementSelectionComponent<T> : BufferComponentAuthoring<T> where T : unmanaged, IBufferElementData, IElementData
{
    [System.Serializable]
    private struct Authoring
    {
        public ElementType Type;
        public int Amount;
    }

    [SerializeField] private List<Authoring> _elements = new List<Authoring>();

    protected override NativeArray<T> AuthorComponent(World world)
    {
        var array = new NativeArray<T>(_elements.Count, Allocator.Temp);
        int index = 0;
        foreach (var authoring in _elements)
        {
            array[index] = new T() { Type = authoring.Type, Count = authoring.Amount };
            index++;
        }
        return array;
    }
}
