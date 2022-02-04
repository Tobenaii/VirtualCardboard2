using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public abstract class GameplayActionTrack<T> : GameplayTrack<GameplayActionTrack<T>.ActionReceiver, GameplayAction<T>, GameplayActionMarker<T>>
{
    [System.Serializable]
    public class ActionReceiver : INotificationReceiver
    {
        private GameplayAction<T> _action;
        public ActionReceiver(GameplayAction<T> action)
        {
            _action = action;
        }

        public void OnNotify(Playable origin, INotification notification, object context)
        {
            _action.Execute((notification as GameplayActionMarker<T>).Value);
        }
    }

    protected override Func<GameplayAction<T>, ActionReceiver> _receiverConstructor => (action) => new ActionReceiver(action);
}
