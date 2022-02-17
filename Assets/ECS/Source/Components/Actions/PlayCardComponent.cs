using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct PlayCard : IComponentData
{
    public Entity Dealer;
    public Entity Card;
}
