using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameplayAction<T> : GameplayObject
{
    public override Type GenericTrackType => typeof(GameplayActionTrack<T>);
    public abstract void Execute(T value);
}
