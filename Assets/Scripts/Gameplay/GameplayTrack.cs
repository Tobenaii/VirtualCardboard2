using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

#if UNITY_EDITOR
[CustomTimelineEditor(typeof(GameplayTrack))]
public class ActionTrackEditor : TrackEditor
{
    public override void OnCreate(TrackAsset track, TrackAsset copiedFrom)
    {
        track.CreateCurves("FakeCurves");
        track.curves.SetCurve(string.Empty, typeof(GameObject), "_fakeCurve", AnimationCurve.Linear(0, 1, 1, 1));
    }
}

#endif

public abstract class GameplayTrack : MarkerTrack
{
    public abstract Type MarkerType { get; }
    public abstract void SetAction(ScriptableObject action);
}

public abstract class GameplayTrack<V, U, W> : GameplayTrack where V : INotificationReceiver where U : ScriptableObject where W : Marker, INotification
{
    protected abstract Func<U, V> _receiverConstructor { get; }
    public override Type MarkerType => typeof(W);


    private V _receiver;
    private U _gameplaySystem;
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        if (Application.isPlaying)
        {
            _receiver = _receiverConstructor(_gameplaySystem);

            PlayableOutput playableOutput = ScriptPlayableOutput.Create(graph, "NotificationOutput");

            //why also here and in "outputs"
            playableOutput.AddNotificationReceiver(_receiver);

            //Create a TimeNotificationBehaviour
            var timeNotificationPlayable = ScriptPlayable<TimeNotificationBehaviour>.Create(graph);
            playableOutput.SetSourcePlayable(graph.GetRootPlayable(0), graph.GetOutputCount() - 1);
            timeNotificationPlayable.GetBehaviour().timeSource = playableOutput.GetSourcePlayable();
            playableOutput.GetSourcePlayable().SetInputCount(playableOutput.GetSourcePlayable().GetInputCount() + 1);
            graph.Connect(timeNotificationPlayable, 0, playableOutput.GetSourcePlayable(), playableOutput.GetSourcePlayable().GetInputCount() - 1);

            var simpleMarkers = this.GetMarkers().OfType<W> ();


            foreach (var marker in simpleMarkers)
            {
                timeNotificationPlayable.GetBehaviour().AddNotification(marker.time, marker);
            }
        }
        return base.CreateTrackMixer(graph, go, inputCount);
    }

    public override IEnumerable<PlayableBinding> outputs
    {
        get
        {
            var playableBinding = ScriptPlayableBinding.Create(name, null, typeof(GameObject));

            return new List<PlayableBinding> { playableBinding };
        }
    }

    public override void SetAction(ScriptableObject gameplaySystem)
    {
        _gameplaySystem = gameplaySystem as U;
    }

    protected override void OnBeforeTrackSerialize()
    {
        if (_gameplaySystem != null)
            this.name = _gameplaySystem.name;
        base.OnBeforeTrackSerialize();
    }
}
