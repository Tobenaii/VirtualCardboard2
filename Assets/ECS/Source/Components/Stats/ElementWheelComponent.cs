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

public class ElementWheelComponent : ComponentAuthoringBase
{
    [SerializeField] private List<ElementData> _elements;
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        var array = new NativeArray<ElementWheel>(_elements.Count, Allocator.Temp);
        for (int i = 0; i < _elements.Count; i++)
        {
            array[i] = new ElementWheel() { Type = _elements[i].Index, Percentage = 100.0f / _elements.Count };
        }
        var buffer = dstManager.AddBuffer<ElementWheel>(entity);
        buffer.AddRange(array);
    }
}
