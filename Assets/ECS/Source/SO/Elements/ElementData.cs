using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementData : DataGroupElement
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Color _color;

    public string Name => _name;
    public Sprite Icon => _icon;
    public Color Color => _color;

    private void OnValidate()
    {
        this.name = _name;
    }
}
