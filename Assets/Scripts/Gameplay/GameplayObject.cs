using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameplayObject : ScriptableObject
{
    public abstract Type GenericTrackType { get; }
}
