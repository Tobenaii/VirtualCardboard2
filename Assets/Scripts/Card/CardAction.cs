using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardAction : ScriptableObject
{
    public abstract void Execute(Entity player);

}

[System.Serializable] [InlineEditor]
public abstract class CardAction<T> : CardAction
{
    public override void Execute(Entity player)
    {
        
    }
    public abstract void Execute(Entity player, T data);
}
