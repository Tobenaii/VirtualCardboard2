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
public struct Element : IElementData, IBufferElementData
{
    public int Type { get; set; }
    public int Count { get; set; }
    public FixedString32 Name { get; set; }
}

public class ElementComponent : ComponentAuthoringBase
{
    [System.Serializable]
    private class ElementAuthoring
    {
        [field: SerializeField] public ElementData Type { get; private set; }
        [field: SerializeField] public int Count { get; private set; }

    }
    [SerializeField] private ElementDataGroup _group;
    [SerializeField] private List<ElementAuthoring> _elements;

    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        var array = new NativeArray<Element>(_group.Count, Allocator.Temp);
        for (int i = 0; i < _group.Count; i++)
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
            }
            else
                array[i] = new Element() { Type = i, Name = _group[i].Name };
        }
        var buffer = dstManager.AddBuffer<Element>(entity);
        buffer.AddRange(array);
    }
}

public abstract class ElementSelectionComponent<T, V> : ComponentAuthoringBase where T : unmanaged, IBufferElementData, IElementData where V : DataGroupElement
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

    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        var array = new NativeArray<T>(_elements.Count, Allocator.Temp);
        for (int i = 0; i < _elements.Count; i++)
        {
            array[i] = new T() { Type = _elements[i].Type.Index, Count = _elements[i].Amount };
        }
        var buffer = dstManager.AddBuffer<T>(entity);
        buffer.AddRange(array);
    }
}
