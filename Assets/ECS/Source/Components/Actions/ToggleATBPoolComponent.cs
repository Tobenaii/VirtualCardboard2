using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct ToggleATBPool : IComponentData
{
    public bool Enable { get; set; }
}

public class ToggleATBPoolComponent : ComponentAuthoringBase
{
    [SerializeField] private bool _enable;
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, new ToggleATBPool() { Enable = _enable });
    }
}
