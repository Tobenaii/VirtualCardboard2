using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Events/Play Card Error Event")]
public class PlayCardErrorEvent : ComponentEvent<PlayCard, PlayCardErrorEventSystem, IStatusMessage>
{
}

[DisableAutoCreation]
public class PlayCardErrorEventSystem : ComponentEventSystem<PlayCard>
{
}