using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[System.Serializable] [InlineProperty]
public abstract class AuthoringValue<T, V>
{
    [SerializeField] [HideLabel] protected T _value;
    public abstract V Author(World world);
}
