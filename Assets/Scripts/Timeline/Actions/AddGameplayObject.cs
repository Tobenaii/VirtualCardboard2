using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class AddGameplayObject : GameplayTrack
{
    public override System.Type MarkerType => throw new System.NotImplementedException();

    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        Rect window = EditorGUIUtility.GetMainWindowPosition();
        var selector = new GameplayActionSelector();
        selector.SelectionCancelled += () => DeletePepegaTrack();
        selector.SelectionConfirmed += action => CreateTrack(action.First());
        selector.ShowInPopup();
        return base.CreateTrackMixer(graph, go, inputCount);
    }

    public override void SetAction(ScriptableObject action)
    {
    }

    private void CreateTrack(GameplayObject gameplayObject)
    {
        var asset = this.timelineAsset;
        var genericTrackType = gameplayObject.GenericTrackType;
        var trackType = genericTrackType.Assembly.GetTypes().FirstOrDefault(x => (x.IsSubclassOf(genericTrackType) || x == genericTrackType) && !x.IsGenericType);
        var track = asset.CreateTrack(trackType, null, gameplayObject.name) as GameplayTrack;
        track.CreateCurves("FakeCurves");
        track.curves.SetCurve(string.Empty, typeof(GameObject), "_fakeCurve", AnimationCurve.Linear(0, 1, 1, 1));
        track.SetAction(gameplayObject);
        DeletePepegaTrack();
    }

    private void DeletePepegaTrack()
    {
        this.timelineAsset.DeleteTrack(this);
        TimelineEditor.Refresh(RefreshReason.ContentsAddedOrRemoved);
    }
}
