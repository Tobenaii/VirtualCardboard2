using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public abstract class GameplayMarker : Marker, INotification, INotificationOptionProvider
{
    [SerializeField] public bool emitOnce;
    [SerializeField] public bool emitInEditor;

    NotificationFlags INotificationOptionProvider.flags =>
    (emitOnce ? NotificationFlags.TriggerOnce : default) |
    (emitInEditor ? NotificationFlags.TriggerInEditMode : default);

    public PropertyName id { get; }
}


public abstract class GameplayMarker<T> : GameplayMarker where T : GameplayTrack
{

}