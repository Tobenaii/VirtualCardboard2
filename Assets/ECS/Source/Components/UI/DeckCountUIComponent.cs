using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class DeckCountUI : IComponentData
{
    public TMPro.TextMeshProUGUI Text { get; set; }
    public string Format { get; set; }
}

public class DeckCountUIComponent : ComponentAuthoringBase
{
    [SerializeField] private TMPro.TextMeshProUGUI _text;
    [SerializeField] private string _format;
    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, new DeckCountUI() { Text = _text, Format = _format });
    }
}
