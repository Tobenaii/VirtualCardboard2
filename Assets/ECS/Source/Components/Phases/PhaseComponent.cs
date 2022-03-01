using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

//TODO: Move timer/name into generic interfaces
public interface IPhase
{
    public float Time { get; set; }
    public float Timer { get; set; }
    public bool HasNextPhase { get; set; }
    public Entity NextPhase { get; set; }
}

public struct Phase : IPhase, IComponentData
{
    public float Time { get; set; }
    public float Timer { get; set; }
    public bool HasNextPhase { get; set; }
    public Entity NextPhase { get; set; }
}

public class PhaseComponent : ComponentAuthoring<Phase>
{
    [SerializeField] private float _time;
    [SerializeField] private ModEntity _nextPhase;
    protected override Phase AuthorComponent(World world)
    {
        if (_nextPhase != null)
            return new Phase() { Time = _time, HasNextPhase = true, NextPhase = _nextPhase.GetPrefab(world.EntityManager, _nextPhase.name) };
        else
            return new Phase() { Time = _time, HasNextPhase = false };
    }
}
