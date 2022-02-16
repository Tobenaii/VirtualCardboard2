using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public struct PhaseNotification : IStatusMessage, IComponentData
{
    public FixedString128 Message { get; set; }
    public IStatusMessage.StatusType Status { get; set; }
}

public class PhaseNotificationComponent : PhaseComponentAuthoring<PhaseNotification>
{
    [SerializeField] [TextArea] private string _message;
    protected override PhaseNotification AuthorComponent(World world)
    {
        return new PhaseNotification() { Message = _message, Status = IStatusMessage.StatusType.Success };
    }
}
