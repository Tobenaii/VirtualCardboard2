using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Timeline;

public class AddGameplayMarker : Marker
{
    public override void OnInitialize(TrackAsset aPent)
    {
        CreateMarker(aPent);
        DeletePepegaMarker(aPent);
    }

    private void CreateMarker(TrackAsset aPent)
    {
        var gameplayTrack = (GameplayTrack)aPent;
        var markerType = typeof(GameplayMarker).Assembly.GetTypes().FirstOrDefault(x => !x.IsAbstract && (x == gameplayTrack.MarkerType || x.IsSubclassOf(gameplayTrack.MarkerType)));
        aPent.CreateMarker(markerType, this.time);
    }

    private void DeletePepegaMarker(TrackAsset aPent)
    {
        aPent.DeleteMarker(this);
        TimelineEditor.Refresh(RefreshReason.ContentsAddedOrRemoved);
    }
}
