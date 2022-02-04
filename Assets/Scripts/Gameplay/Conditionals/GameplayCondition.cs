using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Gameplay Condition")]
public class GameplayCondition : GameplayObject
{
    public bool Condition { get; private set; }

    public override Type GenericTrackType => typeof(GameplayConditionTrack);

    public void StartCondition()
    {
        Condition = false;
    }

    public void EndCondition()
    {
        Condition = true;
    }
}
