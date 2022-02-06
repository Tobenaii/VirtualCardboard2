using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public interface IComponentAuthoring<T, V> : IComponentData where T : struct, IComponentData where V : ComponentAuthoring<T>
{
}

public interface IManagedComponentAuthoring<T, V> : IComponentData where T : IComponentData where V : ManagedComponentAuthoring<T>
{
}

public interface IBufferComponentAuthoring<T, V> : IBufferElementData where T : struct, IBufferElementData where V : BufferComponentAuthoring<T>
{
}
