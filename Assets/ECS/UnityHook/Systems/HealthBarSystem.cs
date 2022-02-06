using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class HealthBarSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((HealthBar healthBar) =>
        {
            var transform = healthBar.transform;
            var health = GetComponentDataFromEntity<Health>(true)[healthBar.entity];
            var width = healthBar.maxWidth * (health.Value / health.MaxValue);
            transform.sizeDelta = new Vector2(width, transform.sizeDelta.y);
        }).WithoutBurst().Run();
    }
}
