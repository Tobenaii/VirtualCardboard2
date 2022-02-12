using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public interface IActionError : IComponentData
{
    public FixedString128 ErrorMessage { get; set; }
}

public struct StartCombat : IComponentData
{
    public Entity InitialPhase;
}

public struct PlayCard : IActionError
{
    public Entity Dealer;
    public Entity Card;
    public bool Failed;
    public FixedString128 ErrorMessage { get; set; }
}

public struct DrawCards : IComponentData
{
    public Entity Entity;
    public int Amount;
}

