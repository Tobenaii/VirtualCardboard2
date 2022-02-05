using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/TargetingSystem")]
public class TargetingSystem : ScriptableObject
{
    [SerializeField] private List<TargetResolver> _resolvers;

    public List<EntityInstance> Execute(EntityInstance player)
    {
        var targets = new List<EntityInstance>();
        foreach (var resolver in _resolvers)
            resolver.GetTargets(player, targets);

        return targets;
    }
}
