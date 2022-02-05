using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttributeSystem : ScriptableObject
{
    public abstract void AttributeChanged(EntityInstance entity, float amount);
}
