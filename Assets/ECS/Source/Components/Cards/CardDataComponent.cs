using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public struct CardData : IComponentData
{
    public FixedString64 name;
    public FixedString512 description;
}

public class CardDataComponent : ComponentAuthoringBase
{
    [SerializeField] private string _name;
    [TextArea]
    [SerializeField] private string _description;
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, new CardData() { name = _name, description = _description });
    }
}
