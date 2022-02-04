using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetResolver : ScriptableObject
{
    public abstract void AddTargets(Entity player, List<Entity> targets);
}
