using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public interface IStatusMessage
{
    public enum StatusType { Success, Failed }
    public FixedString128 Message { get; set; }
    public StatusType Status { get; set; }
}

public struct StartCombat : IComponentData
{
    public Entity InitialPhase;
}

public struct PlayCard : IStatusMessage
{
    public Entity Dealer;
    public Entity Card;
    public FixedString128 Message { get; set; }
    public IStatusMessage.StatusType Status { get; set; }
}

public struct DrawCards : IComponentData
{
    public Entity Entity;
    public int Amount;
}

