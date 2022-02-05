using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Actions/Apply Passive Effect")]
public class ApplyPassiveEffect : CardAction<ApplyPassiveEffect.Data>
{
    [System.Serializable]
    public struct Data
    {
        public TargetingSystem targets;
        public PassiveEffect passiveEffect;
        public float duration;
    }

    public override void Execute(EntityInstance player, Data data)
    {
        foreach (var target in data.targets.Execute(player))
        {
            data.passiveEffect.AddEntity(player, target, data.duration);
        }
    }
}

