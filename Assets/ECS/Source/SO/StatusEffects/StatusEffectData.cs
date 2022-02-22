using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectData : DataGroupElement
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;

    public string Name => _name;
    public Sprite Icon => _icon;

    private void OnValidate()
    {
        this.name = _name;
    }
}
