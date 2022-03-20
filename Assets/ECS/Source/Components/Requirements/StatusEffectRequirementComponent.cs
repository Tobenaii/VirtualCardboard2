using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct TargetStatusEffectRequirement : IComponentData
{
    public int Type { get; set; }
}

public class StatusEffectRequirementComponent : ComponentAuthoringBase
{
    [HideLabel]
    [SerializeField] private StatusEffectData _type;

    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, new TargetStatusEffectRequirement() { Type = _type.Index });
    }
}
