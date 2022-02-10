using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct StartCombat : IComponentData
{
    public Entity InitialPhase;
}

public struct PlayCard : IComponentData
{
    public Entity Dealer;
    public Entity Card;
}

public struct DrawCards : IComponentData
{
    public Entity Entity;
    public int Amount;
}

