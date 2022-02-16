using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatText : MonoBehaviour, IComponentListener<IStat>
{
    //[SerializeField] private ComponentEvent<IStat> _statEvent;
    [SerializeField] private EntityRef _target;

    [SerializeField] private TMPro.TextMeshProUGUI _text;
    [SerializeField] private string _format;

    public void OnComponentChanged(IStat value)
    {
        var text = _format;
        _text.text = text.Replace("{Current}", value.CurrentValue.ToString()).Replace("{Max}", value.MaxValue.ToString());
    }

    private void Start()
    {
        //_statEvent?.Register(_target.Entity, this);
    }
}
