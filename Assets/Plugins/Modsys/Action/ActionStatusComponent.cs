using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
public interface IActionStatus
{
    //Kekw
    public enum StatusType { Success, Failed }
    public FixedString128 Message { get; set; }
    public StatusType Status { get; set; }
}

public struct ActionStatus : IActionStatus, IComponentData
{
    public FixedString128 Message { get; set; }
    public IActionStatus.StatusType Status { get; set; }
}
