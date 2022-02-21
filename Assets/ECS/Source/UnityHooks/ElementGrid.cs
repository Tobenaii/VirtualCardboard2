using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ElementGrid : MonoBehaviour, IComponentChangedListener<IElementData>
{
    [System.Serializable]
    private class ElementData
    {
        [field: SerializeField] public ElementType ElementType { get; private set; }
        [field: SerializeField] public Sprite ElementIcon { get; private set; }
    }

    [SerializeField] private List<ElementData> elementIcons = new List<ElementData>();
    [SerializeField] private Transform _resourceHolder;
    [SerializeField] private Transform _group;
    [SerializeField] private ElementEvent _event;
    [SerializeField] private EntityRef _entity;

    private Dictionary<ElementType, Sprite> _sprites = new Dictionary<ElementType, Sprite>();
    private Dictionary<ElementType, Transform> _groupMap = new Dictionary<ElementType, Transform>();

    private void Awake()
    {
        foreach (var element in elementIcons)
        {
            _sprites.Add(element.ElementType, element.ElementIcon);
            _groupMap.Add(element.ElementType, Instantiate(_group, _resourceHolder));
        }
        Destroy(_group.gameObject);
    }

    private void Start()
    {
        _event.RegisterChanged(_entity.Entity, this);
    }

    public void OnComponentChanged(IElementData value)
    {
        Transform group = _groupMap[value.Type];
        for (int i = 0; i < value.Count; i++)
        {
            group.GetChild(i).gameObject.SetActive(true);
            //TODO: Cache the images;
            group.GetChild(i).GetComponent<Image>().sprite = _sprites[value.Type];
        }

        for (int i = value.Count; i < group.childCount; i++)
        {
            group.GetChild(i).gameObject.SetActive(false);
        }
        if (value.Count == 0)
            group.gameObject.SetActive(false);
        else
        {
            group.gameObject.SetActive(true);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(_resourceHolder as RectTransform);
    }
}
