using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectBar : MonoBehaviour, IComponentChangedListener<IStatusEffectData>
{
    [SerializeField] private ComponentEvent<IStatusEffectData> _event;
    [SerializeField] private EntityRef _target;
    [SerializeField] private StatusEffectGroup _statusEffectGroup;
    [SerializeField] private RectTransform _holder;
    [SerializeField] private Image _image;

    private Dictionary<int, Image> _images = new Dictionary<int, Image>();

    private void Awake()
    {
        for (int i = 0; i < _holder.childCount; i++)
            _holder.GetChild(i).gameObject.SetActive(false);
        for (int i = 0; i < _statusEffectGroup.Count; i++)
        {
            var instance = Instantiate(_image, _holder);
            instance.gameObject.SetActive(false);
            _images.Add(i, instance);
        }
    }

    private void Start()
    {
        _event.RegisterChanged(_target.Entity, this);
    }

    public void OnComponentChanged(IStatusEffectData value)
    {
        var image = _images[value.Type];
        image.sprite = _statusEffectGroup[value.Type].Icon;
        image.gameObject.SetActive(value.Active);
    }
}
