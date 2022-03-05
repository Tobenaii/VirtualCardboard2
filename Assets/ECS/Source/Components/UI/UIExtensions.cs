using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISmoothDamp<T>
{
    public float SmoothTime { get; }
    public T SmoothDamp(T current, T target);
}
