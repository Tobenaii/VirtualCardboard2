using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class RequirementCheckUI : IComponentData
{
    public CanvasGroup BlockingGroup { get; set; }
}

[MovedFrom(true, sourceClassName: "RequirementCheckUIComponent")]
public class CanvasRequirementUIComponent : ComponentAuthoringBase
{
    [SerializeField] private CanvasGroup _blockingGroup;

    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, new RequirementCheckUI() { BlockingGroup = _blockingGroup });
    }
}
