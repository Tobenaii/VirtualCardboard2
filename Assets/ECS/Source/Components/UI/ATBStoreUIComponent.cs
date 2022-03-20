using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

public class ATBStoreUI : IComponentData
{
    public Image PoolImage { get; set; }
    public TMPro.TextMeshProUGUI PoolText { get; set; }
    public string PoolTextFormat { get; set; }
    public List<Image> BarImages { get; set; }

}

public class ATBStoreUIComponent : ComponentAuthoringBase
{
    [SerializeField] private Image _poolImage;
    [SerializeField] private TMPro.TextMeshProUGUI _poolText;
    [SerializeField] private string _poolTextFormat;
    [SerializeField] private List<Image> _barImages;

    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, new ATBStoreUI() { PoolImage = _poolImage, PoolText = _poolText, PoolTextFormat = _poolTextFormat, BarImages = _barImages });
    }

    public override void UpdateComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.SetComponentData(entity, new ATBStoreUI() { PoolImage = _poolImage, PoolText = _poolText, PoolTextFormat = _poolTextFormat, BarImages = _barImages });

    }
}
