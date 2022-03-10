using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class RequirementCheckUI : IComponentData
{
    public CanvasGroup BlockingGroup { get; set; }
}

public class RequirementCheckUIComponent : ManagedComponentAuthoring<RequirementCheckUI>
{
    [SerializeField] private CanvasGroup _blockingGroup;

    protected override RequirementCheckUI AuthorComponent(World world)
    {
        return new RequirementCheckUI() { BlockingGroup = _blockingGroup };
    }
}
