using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public interface INotification
{
    public FixedString128 Notification { get; set; }
}

public struct PhaseNotification : INotification, IComponentData
{
    public FixedString128 Notification { get; set; }
}

public class PhaseNotificationComponent : ComponentAuthoringBase
{
    [TextArea]
    [SerializeField] private string _notification;

    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, new PhaseNotification() { Notification = _notification });
    }
}
