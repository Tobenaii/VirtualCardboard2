using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[InlineProperty]
public abstract class CardActionData : ScriptableObject
{
    [SerializeField] [HideInInspector] private CardAction _action;

    public virtual void RegisterCardAction(CardAction action)
    {
        _action = action;
    }

    public virtual void Execute(Entity player)
    {
        _action.Execute(player);
    }
}

public abstract class CardActionData<T, V> : CardActionData where T : CardAction
{
    [SerializeField] [HideLabel] private V _data;
    [SerializeField] [HideInInspector] private CardAction<V> _actionData;
    public override void Execute(Entity player)
    {
        _actionData.Execute(player, _data);
    }

    public override void RegisterCardAction(CardAction action)
    {
        _actionData = action as CardAction<V>;
    }
}

