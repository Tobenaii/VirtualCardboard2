using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct ReceiveElementFromWheel : IComponentData
{
}

public class ReceiveElementFromWheelComponent : ComponentAuthoring<ReceiveElementFromWheel>
{
    protected override ReceiveElementFromWheel AuthorComponent(World world)
    {
        return new ReceiveElementFromWheel();
    }
}
