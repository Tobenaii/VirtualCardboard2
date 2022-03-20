using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayCardUI : IComponentData { }

public class PlayCardUIComponent : ComponentAuthoringBase
{
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponent<PlayCardUI>(entity);
    }
}
