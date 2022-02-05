using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Actions/AttributeAction")]
public class AttributeAction : CardAction<AttributeAction.Data>
{
    [System.Serializable] [InlineProperty]
    public struct Data
    {
        [field: SerializeField] public float Amount { get; private set; }
    }

    [SerializeField] private TargetingSystem _targetingSystem;
    [SerializeField] private AttributeResolver _attributeResolver;

    public override void Execute(EntityInstance player, Data data)
    {
        foreach (var target in _targetingSystem.Execute(player))
        {
            _attributeResolver.Resolve(player, target, data.Amount);
        }
    }
}
