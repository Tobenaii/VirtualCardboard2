using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[InlineEditor]
public abstract class AttributeResolverCheck : ScriptableObject
{
    protected enum CheckOn { Dealer, Receiver }

    [SerializeField] private CheckOn _checkOn;
    public float Resolve(EntityInstance dealer, EntityInstance receiver, float amount)
    {
        return Resolve(_checkOn == CheckOn.Dealer ? dealer : receiver, amount);
    }

    protected abstract float Resolve(EntityInstance entity, float amount);
}
