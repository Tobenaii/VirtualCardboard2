using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(UISystemGroup))]
public class HealthTextUISystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((HealthTextUI ui) =>
        {
            if (!EntityManager.GetChunk(ui.Target).DidChange(GetComponentTypeHandle<Health>(true), LastSystemVersion))
                return;
            var health = GetComponentDataFromEntity<Health>(true)[ui.Target];

            var text = ui.Format;
            ui.TextMesh.text = text.Replace("{Current}", health.CurrentValue.ToString()).Replace("{Max}", health.MaxValue.ToString());
        }).WithoutBurst().Run();
    }
}
