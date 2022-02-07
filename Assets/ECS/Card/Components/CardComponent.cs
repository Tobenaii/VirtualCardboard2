using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public struct Card : IComponentData
{
    public FixedString64 name;
    public FixedString512 description;
}

public class CardComponent : CardEffectAuthoring<Card>
{
    [SerializeField] private string _name;
    [SerializeField] [TextArea] private string _description;

    protected override Card AuthorComponent(World world)
    {
        return new Card() { name = _name, description = _description };
    }
}
