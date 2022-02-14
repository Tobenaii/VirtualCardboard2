using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public struct PhaseData : IComponentData
{
    public float Time;
    public float Timer;
    public bool HasNextPhase;
    public Entity NextPhase;
}

public class PhaseDataComponent : ComponentAuthoring<PhaseData>
{
    [SerializeField] private float _time;
    [SerializeField] private ModEntity _nextPhase;
    protected override PhaseData AuthorComponent(World world)
    {
        if (_nextPhase != null)
            return new PhaseData() { Time = _time, HasNextPhase = true, NextPhase = _nextPhase.GetPrefab(world.EntityManager) };
        else
            return new PhaseData() { Time = _time, HasNextPhase = false };
    }
}
