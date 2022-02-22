using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public struct ApplyStatusEffect : IComponentData
{
    public int Type { get; set; }
}

public class ApplyStatusEffectComponent : ComponentAuthoring<ApplyStatusEffect>
{
    [SerializeField] private StatusEffectData _effect;

    protected override ApplyStatusEffect AuthorComponent(World world)
    {
        return new ApplyStatusEffect() { Type = _effect.Index };
    }
}
