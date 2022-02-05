using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierCheck : AttributeResolverCheck
{
    [SerializeField] private Attribute _multiplierAttribute;

    protected override float Resolve(EntityInstance entity, float amount)
    {
        return amount * _multiplierAttribute[entity];
    }
}
