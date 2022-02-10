using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerText : MonoBehaviour, IComponentListener<ICollectionContainer>
{
    [SerializeField] private ComponentEvent<ICollectionContainer> _containerEvent;
    [SerializeField] private EntityRef _target;

    [SerializeField] private TMPro.TextMeshProUGUI _text;
    [SerializeField] private string _format;

    public void OnComponentChanged(ICollectionContainer value)
    {
        var text = _format;
        _text.text = text.Replace("{Current}", value.CurrentCount.ToString()).Replace("{Max}", value.MaxCount.ToString());
    }

    private void Start()
    {
        _containerEvent.Register(_target.Entity, this);
    }
}
