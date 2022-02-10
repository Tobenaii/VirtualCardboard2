using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ElementGrid : MonoBehaviour, IComponentListener<IElementData>
{
    [System.Serializable]
    private class ElementData
    {
        [field: SerializeField] public ElementType ElementType { get; private set; }
        [field: SerializeField] public Sprite ElementIcon { get; private set; }
    }

    [SerializeField] private List<ElementData> elementIcons = new List<ElementData>();
    [SerializeField] private Transform _resourceHolder;
    [SerializeField] private ElementEvent _event;
    [SerializeField] private EntityRef _entity;

    private Dictionary<ElementType, Sprite> _sprites = new Dictionary<ElementType, Sprite>();
    private Dictionary<ElementType, List<Image>> _activeTypes = new Dictionary<ElementType, List<Image>>();

    private Stack<Image> _imagePool = new Stack<Image>();

    private void Awake()
    {
        foreach (var element in elementIcons)
            _sprites.Add(element.ElementType, element.ElementIcon);

        for (int i = 0; i < _resourceHolder.childCount; i++)
        {
            Image image = _resourceHolder.GetChild(i).GetComponent<Image>();
            image.gameObject.SetActive(false);
            _imagePool.Push(image);
        }
    }

    private void Start()
    {
        _event.Register(_entity.Entity, this);
    }

    public void OnComponentChanged(IElementData value)
    {
        List<Image> images;
        int index = 0;
        if (_activeTypes.TryGetValue(value.Type, out images))
        {
            images = new List<Image>();
            _activeTypes.Add(value.Type, images);
            index = images.Last().transform.GetSiblingIndex();
        }
        for (int i = 0; i < value.Count; i++)
        {
            var image = _imagePool.Pop();
            image.sprite = _sprites[value.Type];
            image.transform.SetSiblingIndex(index + 1);
            image.gameObject.SetActive(true);
            index++;
        }

    }
}
