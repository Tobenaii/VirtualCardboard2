using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

public class ElementWheelUI : IComponentData
{
    public ElementDataGroup ElementData { get; set; }
    public List<Image> Images { get; set; }
    public List<SmoothDamperooski> SmoothDamperooskis { get; set; }

    public class SmoothDamperooski : ISmoothDamp<float>
    {
        private float _fillVelocity;
        public float SmoothTime { get; set; }

        public float SmoothDamp(float current, float target)
        {
            return Mathf.SmoothDamp(current, target, ref _fillVelocity, SmoothTime);
        }
    }
}

public class ElementWheelUIComponent : ComponentAuthoringBase
{
    [SerializeField] private ElementDataGroup _elements;
    [SerializeField] private List<Image> _images;
    [SerializeField] private float _smoothTime;

    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        var damperooskis = new List<ElementWheelUI.SmoothDamperooski>();
        for (int i = 0; i < _images.Count; i++)
            damperooskis.Add(new ElementWheelUI.SmoothDamperooski() { SmoothTime = _smoothTime });

        dstManager.AddComponentData(entity, new ElementWheelUI() { Images = _images, SmoothDamperooskis = damperooskis, ElementData = _elements });
    }
}
