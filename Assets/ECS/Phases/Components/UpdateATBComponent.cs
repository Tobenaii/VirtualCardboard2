using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct UpdateATB : IComponentData
{
}

public class UpdateATBComponent : PhaseComponentAuthoring<UpdateATB>
{
    protected override UpdateATB AuthorComponent(World world)
    {
        return new UpdateATB();
    }
}