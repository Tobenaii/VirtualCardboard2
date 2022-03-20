using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct DealDamage : IComponentData
{
    public int Amount;
}

public class DealDamageComponent : ComponentAuthoringBase
{
    [System.Serializable]
    private struct DamageBuff
    {
        [HideLabel]
        public int Amount;
    }
    [SerializeField] private int _amount;
    [SerializeField] private List<DamageBuff> _buffs;
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, new DealDamage() { Amount = _amount });
    }
}
