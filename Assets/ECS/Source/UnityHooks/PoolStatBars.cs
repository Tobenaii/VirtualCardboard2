using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoolStatBars : MonoBehaviour, IComponentChangedListener<IStat>, IComponentChangedListener<IStatPool>
{
    [Header("Events")]
    [SerializeField] private EntityRef _entity;
    [SerializeField] private ComponentEvent<IStat> _statEvent;
    [SerializeField] private ComponentEvent<IStatPool> _poolEvent;
    [Header("Components")]
    [SerializeField] private List<Image> _bars;
    [SerializeField] private Image _poolImage;
    [SerializeField] private TMPro.TextMeshProUGUI _poolText;
    [Header("Data")]
    [SerializeField] private string _poolTextFormat;

    private IStat _stat;
    private IStatPool _pool;

    private void Awake()
    {
        foreach (var bar in _bars)
            bar.fillAmount = 0;
    }

    private void Start()
    {
        _statEvent.RegisterChanged(_entity.Entity, this);
        _poolEvent.RegisterChanged(_entity.Entity, this);
    }

    //I would've loved to make this not hold any state but alas the components are TOO decoupled :P
    //We could allow listening for multiple components through the same event system but ceebs
    private void UpdateBars()
    {
        if (_stat == null || _pool == null)
            return;
        _poolText.text = _poolTextFormat.Replace("{Current}", _pool.CurrentCount.ToString()).Replace("{Max}", _pool.MaxCount.ToString());
        _poolImage.fillAmount = _pool.ChargeTimer / _pool.ChargeTime;
        for (int i = 0; i < _stat.CurrentValue; i++)
        {
            _bars[i].fillAmount = 1;
        }
        for (int i = _stat.CurrentValue; i < _stat.MaxValue; i++)
        {
            _bars[i].fillAmount = 0;
        }
        if (_stat.CurrentValue == _stat.MaxValue)
            return;
        //_bars[_stat.CurrentValue].fillAmount = _pool.ChargeTimer / _pool.ChargeTime;
    }

    public void OnComponentChanged(IStat value)
    {
        _stat = value;
        UpdateBars();
    }

    public void OnComponentChanged(IStatPool value)
    {
        _pool = value;
        UpdateBars();
    }
}
