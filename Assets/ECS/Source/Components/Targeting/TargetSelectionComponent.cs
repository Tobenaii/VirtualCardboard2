using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public interface ITargetSelection
{
    public int Index { get; set; }
}

public struct TargetSelection : ITargetSelection, IComponentData
{
    public int Index { get; set; }

}

public class TargetSelectionComponent : ComponentAuthoringBase
{
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponent<TargetSelection>(entity);
    }
}
