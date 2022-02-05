using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetResolver : ScriptableObject
{
    public abstract void GetTargets(EntityInstance player, List<EntityInstance> targets);
}
