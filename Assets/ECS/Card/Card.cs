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
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _consumeATB;
    public override void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new CardData() { name = _name, description = _description });
        dstManager.AddComponentData(entity, new ConsumeATB() { amount = _consumeATB });
        base.Convert(entity, dstManager, conversionSystem);
    }
}
