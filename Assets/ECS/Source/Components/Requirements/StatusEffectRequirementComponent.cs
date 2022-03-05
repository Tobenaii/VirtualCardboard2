using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct TargetStatusEffectRequirement : IComponentData
{
    public int Type { get; set; }
}

public class StatusEffectRequirementComponent : ComponentAuthoring<TargetStatusEffectRequirement>
{
    [HideLabel]
    [SerializeField] private StatusEffectData _type;

    protected override TargetStatusEffectRequirement AuthorComponent(World world)
    {
        return new TargetStatusEffectRequirement() { Type = _type.Index };
    }
}
