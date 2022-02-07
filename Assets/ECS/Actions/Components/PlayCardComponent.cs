using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct PlayCard : IComponentData
{
    public Entity dealer;
    public Entity card;
}

