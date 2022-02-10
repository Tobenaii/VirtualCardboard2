using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public abstract class UnitBufferComponentAuthoring<T> : BufferComponentAuthoring<T> where T : struct, IBufferElementData
{
}
