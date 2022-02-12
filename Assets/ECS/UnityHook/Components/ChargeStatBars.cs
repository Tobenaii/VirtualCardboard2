using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeStatBars : MonoBehaviour, IComponentListener<IChargeStat>
{
    [SerializeField] private List<Image> _bars;
    [SerializeField] private TMPro.TextMeshProUGUI _poolText;
    [SerializeField] private string _poolTextFormat;
    [SerializeField] private Image _poolImage;
    [SerializeField] private EntityRef _entity;
    [SerializeField] private ComponentEvent<IChargeStat> _event;


    private void Awake()
    {
        foreach (var bar in _bars)
            bar.fillAmount = 0;
    }

    private void Start()
    {
        _event.Register(_entity.Entity, this);
    }

    public void OnComponentChanged(IChargeStat value)
    {
        _poolText.text = _poolTextFormat.Replace("{Current}", value.Pool.ToString()).Replace("{Max}", value.MaxPool.ToString());
        _poolImage.fillAmount = value.ChargeTimer / value.ChargeTime;
        for (int i = 0; i < value.Charges; i++)
        {
            _bars[i].fillAmount = 1;
        }
        for (int i = value.Charges; i < value.MaxCharges; i++)
        {
            _bars[i].fillAmount = 0;
        }
        if (value.Charges == value.MaxCharges)
            return;
        _bars[value.Charges].fillAmount = value.ChargeTimer / value.ChargeTime;
    }
}
