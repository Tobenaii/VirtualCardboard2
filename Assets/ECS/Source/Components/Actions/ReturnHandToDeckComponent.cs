using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct ReturnHandToDeck : IComponentData
{
}

public class ReturnHandToDeckComponent : ComponentAuthoring<ReturnHandToDeck>
{
    protected override ReturnHandToDeck AuthorComponent(World world)
    {
        return new ReturnHandToDeck();
    }
}
