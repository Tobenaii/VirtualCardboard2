using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct StatusEffectRequirement : IComponentData
{
    public enum On { Target, Dealer }
    public int Type { get; set; }
    public On CheckOn { get; set; }
}

public class StatusEffectRequirementComponent : ComponentAuthoring<StatusEffectRequirement>
{
    [HideLabel]
    [SerializeField] private StatusEffectData _type;
    [HideLabel]
    [SerializeField] private StatusEffectRequirement.On _checkOn;

    protected override StatusEffectRequirement AuthorComponent(World world)
    {
        return new StatusEffectRequirement() { Type = _type.Index, CheckOn = _checkOn };
    }
}
