using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Phase")]
public class Phase : ModEntity<PhaseArchetype>
{
    [PropertyOrder(10)]
    [SerializeField] private float _time;
    [SerializeField] private Phase _nextPhase;

    public override void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        if (_nextPhase != null)
            dstManager.AddComponentData(entity, new PhaseData() { Time = _time, Timer = 0, HasNextPhase = true, NextPhase = _nextPhase.GetPrefab(dstManager) });
        else
            dstManager.AddComponentData(entity, new PhaseData() { Time = _time, Timer = 0, HasNextPhase = false });
        base.Convert(entity, dstManager, conversionSystem);
    }
}
