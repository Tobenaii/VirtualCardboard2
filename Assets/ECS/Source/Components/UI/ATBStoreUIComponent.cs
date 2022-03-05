using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

public class ATBStoreUI : IComponentData
{
    public Entity Target { get; set; }
    public Image PoolImage { get; set; }
    public TMPro.TextMeshProUGUI PoolText { get; set; }
    public string PoolTextFormat { get; set; }
    public List<Image> BarImages { get; set; }

}

public class ATBStoreUIComponent : ManagedComponentAuthoring<ATBStoreUI>
{
    [SerializeField] private EntityRef _target;
    [SerializeField] private Image _poolImage;
    [SerializeField] private TMPro.TextMeshProUGUI _poolText;
    [SerializeField] private string _poolTextFormat;
    [SerializeField] private List<Image> _barImages;

    protected override ATBStoreUI AuthorComponent(World world)
    {
        return new ATBStoreUI() { PoolImage = _poolImage, PoolText = _poolText, PoolTextFormat = _poolTextFormat, BarImages = _barImages, Target = _target.Entity };
    }
}
