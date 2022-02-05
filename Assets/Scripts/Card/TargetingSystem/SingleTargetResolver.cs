using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/TargetResolver/Single Target")]
public class SingleTargetResolver : TargetResolver
{
    public EntityInstance Target => _currentTarget;
    private EntityInstance _currentTarget;

    public void SetTarget(EntityInstance instance)
    {
        _currentTarget = instance;
    }

    public override void GetTargets(EntityInstance player, List<EntityInstance> targets)
    {
        targets.Add(_currentTarget);
    }
}
