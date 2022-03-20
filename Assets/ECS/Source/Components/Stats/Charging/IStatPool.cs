using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public interface IStatPool
{
    public bool Enabled { get; set; }
    public int MaxCount { get; set; }
    public int CurrentCount { get; set; }
    public float ChargeTime { get; set; }
    public float ChargeTimer { get; set; }
}

public abstract class StatPoolAuthoring<T> : ComponentAuthoringBase where T : unmanaged, IStatPool, IComponentData
{
    [SerializeField] private int _maxCount;
    [SerializeField] private float _chargeTime;

    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, new T() { MaxCount = _maxCount, CurrentCount = 0, ChargeTime = _chargeTime });
    }
}
