using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class HealthChangeEventSystem : SystemBase
{
    public IComponentEvent<Health> healthChangedEvent;

    protected override void OnUpdate()
    {
        Entities.WithChangeFilter<Health>().ForEach((Entity entity, in Health health) =>
        {
            healthChangedEvent.OnComponentChanged(health, health, entity);
        }).WithoutBurst().Run();
    }
}
