using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class HealthTextUI : IComponentData
{
    public Entity Target { get; set; }
    public TMPro.TextMeshProUGUI TextMesh { get; set; }
    public string Format { get; set; }
}

public class HealthTextUIComponent : ManagedComponentAuthoring<HealthTextUI>
{
    [SerializeField] private EntityRef _target;
    [SerializeField] private TMPro.TextMeshProUGUI _textMesh;
    [SerializeField] private string _format = "{Current}/{Max}";
    protected override HealthTextUI AuthorComponent(World world)
    {
        return new HealthTextUI() { Target = _target.Entity, TextMesh = _textMesh, Format = _format };
    }
}
