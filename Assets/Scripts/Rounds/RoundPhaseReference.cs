using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Round Phase Reference")]
public class RoundPhaseReference : ScriptableObject
{
    public RoundPhase RoundPhase { get; private set; }

    public void SetReference(RoundPhase phase)
    {
        RoundPhase = phase;
    }
}
