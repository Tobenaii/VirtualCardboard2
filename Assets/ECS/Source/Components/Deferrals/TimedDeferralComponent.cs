using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct TimedDeferral : IComponentData
{
    public float Delay { get; set; }
    public float Timer { get; set; }
}

public class TimedDeferralComponent : ComponentAuthoring<TimedDeferral>
{
    [SerializeField] private float _delay;

    protected override TimedDeferral AuthorComponent(World world)
    {
        return new TimedDeferral() { Delay = _delay };
    }
}
