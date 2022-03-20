using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public struct ApplyStatusEffect : IComponentData
{
    public int Type { get; set; }
}

public class ApplyStatusEffectComponent : ComponentAuthoringBase
{
    [SerializeField] private StatusEffectData _effect;

    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, new ApplyStatusEffect() { Type = _effect.Index });
    }
}
