using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(ReceiverResolverGroup))]
public class ArmourSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref DealDamage damage, in Target target) =>
        {
            if (HasComponent<Armour>(target.target))
            {
                var armour = GetComponentDataFromEntity<Armour>(true)[target.target];
                if (armour.Value >= damage.amount)
                {
                    armour.Value -= damage.amount;
                    damage.amount = 0;
                }
                else
                {
                    armour.Value -= damage.amount;
                    armour.Value = 0;
                    damage.amount = armour.Value * -1;
                }
                SetComponent<Armour>(target.target, armour);
            }
        }).Schedule();
    }
}
