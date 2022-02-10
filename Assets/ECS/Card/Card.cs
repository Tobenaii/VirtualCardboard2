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

[CreateAssetMenu(menuName = "VC2/Card")]
public class Card : ModEntity<CardEffect>
{
    [PropertyOrder(-100)]
    [SerializeField] private string _name;
    [TextArea] [PropertyOrder(-100)]
    [SerializeField] private string _description;
    public override void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new CardData() { name = _name, description = _description });
        base.Convert(entity, dstManager, conversionSystem);
    }
}
