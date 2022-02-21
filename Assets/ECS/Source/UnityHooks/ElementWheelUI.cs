using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ElementWheelUI : MonoBehaviour, IComponentChangedListener<IElementWheel>
{
    [System.Serializable]
    private struct ElementColour
    {
        public ElementType element;
        public Color color;
    }

    private class Element
    {
        public ElementType element;
        public Color color;
        public Image image;
        public int index;
        public float target;

        private float _fillVelocity;
        public void Update(float smoothTime)
        {
            image.fillAmount = Mathf.SmoothDamp(image.fillAmount, target, ref _fillVelocity, smoothTime);
        }
    }

    [SerializeField] private float _smoothTime;
    [SerializeField] private ComponentEvent<IElementWheel> _event;
    [SerializeField] private List<Image> _images;
    [SerializeField] private List<ElementColour> _colours;

    private Dictionary<ElementType, Color> _colourMap = new Dictionary<ElementType, Color>();
    private Dictionary<ElementType, Element> _elementMap = new Dictionary<ElementType, Element>();
    private Stack<Image> _elementImagePool = new Stack<Image>();


    private void Awake()
    {
        foreach (var element in _images)
            _elementImagePool.Push(element);

        foreach (var colour in _colours)
            _colourMap.Add(colour.element, colour.color);
    }

    private void Start()
    {
        _event.Register(this);
    }

    private void Update()
    {
        foreach (var element in _elementMap.Values)
        {
            element.Update(_smoothTime);
        }
    }

    public void OnComponentChanged(IElementWheel value)
    {
        if (!_elementMap.ContainsKey(value.Type))
        {
            _elementMap.Add(value.Type, new Element() { color = _colourMap[value.Type], element = value.Type, image = _elementImagePool.Pop(), index = _elementMap.Count });
        }
        var element = _elementMap[value.Type];
        float start = 0;
        for (int i = 0; i < element.index; i++)
        {
            start += _elementMap.Values.ElementAt(i).target;
        }
        element.target = start + value.Percentage / 100.0f;
        element.image.color = element.color;
    }
}
