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
public class HandCardDataUIComponent : ManagedComponentAuthoring<HandCardDataUI>
{
    [SerializeField] private TMPro.TextMeshProUGUI _title;
    [SerializeField] private TMPro.TextMeshProUGUI _description;

    protected override HandCardDataUI AuthorComponent(World world)
    {
        return new HandCardDataUI() { Title = _title, Description = _description };
    }
}
