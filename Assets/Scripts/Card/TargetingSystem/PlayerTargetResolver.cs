using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/TargetResolver/Player")]
public class PlayerTargetResolver : TargetResolver
{
    public override void GetTargets(EntityInstance player, List<EntityInstance> targets)
    {
        targets.Add(player);
        Debug.Log($"Targeted {player.Name}");
    }
}
