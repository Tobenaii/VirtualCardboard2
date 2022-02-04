using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class RequirementResolver : ScriptableObject
{
    public abstract bool CanResolve(List<Entity> targets);
    public abstract void Resolve(List<Entity> targets);
}
