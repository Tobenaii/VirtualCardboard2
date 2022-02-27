using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public interface IElementData
{
    public int Type { get; set; }
    public int Count { get; set; }
}

[InternalBufferCapacity(10)] [System.Serializable]
public struct Element : IElementData, IBufferElementData, IBufferFlag
{
    public int Type { get; set; }
    public int Count { get; set; }
    public FixedString32 Name { get; set; }
    public IBufferFlag.Flag BufferFlag { get; set; }
}

public class ElementComponent : BufferComponentAuthoring<Element>
{
    [System.Serializable]
    private class ElementAuthoring
    {
        [field: SerializeField] public ElementData Type { get; private set; }
        [field: SerializeField] public int Count { get; private set; }

    }
    [SerializeField] private ElementDataGroup _group;
    [SerializeField] private List<ElementAuthoring> _elements;

    protected override NativeArray<Element> AuthorComponent(World world)
    {
        var array = new NativeArray<Element>(_group.Count, Allocator.Temp);
        int index = 0;
        for (int i = 0; i < array.Length; i++)
        {
            var authoring = _elements.Where(x => x.Type.Index == i).FirstOrDefault();
            if (authoring != null)
            {
                array[i] = new Element()
                {
                    Type = authoring.Type.Index,
                    Count = authoring.Count,
                    Name = _group[i].Name,
                };
                index++;
            }
            else
                array[i] = new Element() { Type = i, Name = _group[i].Name };
        }
        return array;
    }
}

public abstract class ElementSelectionComponent<T, V> : BufferComponentAuthoring<T> where T : unmanaged, IBufferElementData, IElementData where V : DataGroupElement
{
    [System.Serializable]
    private struct Authoring
    {
        [HideLabel]
        public V Type;
        [HideLabel]
        public int Amount;
    }

    [ListDrawerSettings(Expanded = true, ShowItemCount = false)]
    [SerializeField] private List<Authoring> _elements = new List<Authoring>();

    protected override NativeArray<T> AuthorComponent(World world)
    {
        var array = new NativeArray<T>(_elements.Count, Allocator.Temp);
        int index = 0;
        foreach (var authoring in _elements)
        {
            array[index] = new T() { Type = authoring.Type.Index, Count = authoring.Amount };
            index++;
        }
        return array;
    }
}
