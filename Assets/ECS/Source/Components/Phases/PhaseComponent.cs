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

public class PhaseComponent : ComponentAuthoringBase
{
    [SerializeField] private float _time;
    [SerializeField] private EntityData _nextPhase;
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        if (_nextPhase != null)
            dstManager.AddComponentData(entity, new Phase() { Time = _time, HasNextPhase = true, NextPhase = _nextPhase.GetPrefab(dstManager) });
        else
            dstManager.AddComponentData(entity, new Phase() { Time = _time, HasNextPhase = false });
    }
}
