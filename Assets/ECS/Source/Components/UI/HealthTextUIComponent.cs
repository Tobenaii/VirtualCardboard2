using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class HealthTextUI : IComponentData
{
    public TMPro.TextMeshProUGUI TextMesh { get; set; }
    public string Format { get; set; }
}

public class HealthTextUIComponent : ComponentAuthoringBase
{
    [SerializeField] private TMPro.TextMeshProUGUI _textMesh;
    [SerializeField] private string _format = "{Current}/{Max}";
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, new HealthTextUI() { TextMesh = _textMesh, Format = _format });
    }
}
