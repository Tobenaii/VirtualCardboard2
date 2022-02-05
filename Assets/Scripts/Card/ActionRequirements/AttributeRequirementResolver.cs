using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "VC2/RequirementResolver/Attribute")]
public class AttributeRequirementResolver : RequirementResolver
{
    [SerializeField] private Attribute _attribute;
    [SerializeField] private float _amount;

    public override bool CanResolve(List<EntityInstance> targets)
    {
        foreach (var entity in targets)
        {
            Debug.Log($"Checking if {entity.Name} has {_amount} {_attribute}");
            if (_attribute[entity] < _amount)
                return false;
        }
        return true;
    }

    public override void Resolve(List<EntityInstance> targets)
    {
        foreach (var entity in targets)
        {
            //entity.SetAttribute(_attribute, entity.GetAttribute(_attribute) - _amount);
        }
    }
}
