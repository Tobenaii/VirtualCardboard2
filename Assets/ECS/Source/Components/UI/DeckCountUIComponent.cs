using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class DeckCountUI : IComponentData
{
    public TMPro.TextMeshProUGUI Text { get; set; }
    public string Format { get; set; }
}

public class DeckCountUIComponent : ManagedComponentAuthoring<DeckCountUI>
{
    [SerializeField] private TMPro.TextMeshProUGUI _text;
    [SerializeField] private string _format;
    protected override DeckCountUI AuthorComponent(World world)
    {
        return new DeckCountUI() { Text = _text, Format = _format };
    }
}
