using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCheck : AttributeResolverCheck
{
    [SerializeField] private Attribute _shieldingAttribute;
    protected override float Resolve(EntityInstance entity, float amount)
    {
        if (amount > 0)
            return amount;
        float shield = _shieldingAttribute[entity];
        shield += amount;
        float leftover = shield < 0 ? shield : 0;
        shield = 0;
        _shieldingAttribute[entity] = shield;
        return leftover;
    }
}
