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

public class PhaseNotificationComponent : ComponentAuthoring<PhaseNotification>
{
    [TextArea]
    [SerializeField] private string _notification;

    protected override PhaseNotification AuthorComponent(World world)
    {
        return new PhaseNotification() { Notification = _notification };
    }
}
