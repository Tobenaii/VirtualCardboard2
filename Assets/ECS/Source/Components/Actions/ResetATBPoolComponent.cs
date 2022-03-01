using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct ResetATBPool : IComponentData { }

public class ResetATBPoolComponent : ComponentAuthoring<ResetATBPool>
{
    protected override ResetATBPool AuthorComponent(World world)
    {
        return new ResetATBPool();
    }
}
