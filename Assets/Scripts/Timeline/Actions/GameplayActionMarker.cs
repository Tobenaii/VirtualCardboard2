using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public abstract class GameplayActionMarker<T> : GameplayMarker<GameplayActionTrack<T>>
{
    [SerializeField] private T _value;
    public T Value => _value;
}
