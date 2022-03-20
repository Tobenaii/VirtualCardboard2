using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct TimedDeferral : IComponentData
{
    public float Delay { get; set; }
    public float Timer { get; set; }
}

public class TimedDeferralComponent : ComponentAuthoringBase
{
    [SerializeField] private float _delay;

    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, new TimedDeferral() { Delay = _delay });
    }
}
