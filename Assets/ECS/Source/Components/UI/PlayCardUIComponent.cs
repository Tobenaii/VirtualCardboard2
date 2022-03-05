using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayCardUI : IComponentData { }

public class PlayCardUIComponent : ManagedComponentAuthoring<PlayCardUI>
{
    protected override PlayCardUI AuthorComponent(World world)
    {
        return new PlayCardUI();
    }
}
