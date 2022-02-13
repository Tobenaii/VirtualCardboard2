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