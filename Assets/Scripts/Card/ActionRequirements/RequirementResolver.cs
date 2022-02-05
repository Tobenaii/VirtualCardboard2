using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class RequirementResolver : ScriptableObject
{
    public abstract bool CanResolve(List<EntityInstance> targets);
    public abstract void Resolve(List<EntityInstance> targets);
}
