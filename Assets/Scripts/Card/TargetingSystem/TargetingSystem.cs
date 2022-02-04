using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/TargetingSystem")]
public class TargetingSystem : ScriptableObject
{
    [SerializeField] private List<TargetResolver> _resolvers;

    public List<Entity> Execute(Entity player)
    {
        var targets = new List<Entity>();
        foreach (var resolver in _resolvers)
            resolver.AddTargets(player, targets);

        return targets;
    }
}
