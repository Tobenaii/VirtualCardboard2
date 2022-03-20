using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class HandCardDataUI : IComponentData
{
    public TMPro.TextMeshProUGUI Title { get; set; }
    public TMPro.TextMeshProUGUI Description { get; set; }
}

[MovedFrom(true, sourceClassName: "CardDataUIComponent")]
public class HandCardDataUIComponent : ComponentAuthoringBase
{
    [SerializeField] private TMPro.TextMeshProUGUI _title;
    [SerializeField] private TMPro.TextMeshProUGUI _description;

    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, new HandCardDataUI() { Title = _title, Description = _description });
    }
}
