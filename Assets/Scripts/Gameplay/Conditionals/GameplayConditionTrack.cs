using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[HideInMenu]
public class GameplayConditionTrack : GameplayTrack<GameplayConditionTrack.ConditionReceiver, GameplayCondition, GameplayConditionMarker>
{
    protected override Func<GameplayCondition, ConditionReceiver> _receiverConstructor => (condition) => new ConditionReceiver(condition);

    public class ConditionReceiver : INotificationReceiver
    {
        private GameplayCondition _condition;
        public ConditionReceiver(GameplayCondition condition)
        {
            _condition = condition;
        }

        public void OnNotify(Playable origin, INotification notification, object context)
        {
            if (!_condition.Condition)
            {
                var time = (notification as Marker).time;
                var root = origin.GetGraph().GetRootPlayable(0);
                root.SetTime(root.GetTime() - (root.GetTime() - time) - 0.1f);
            }
        }
    }
}
