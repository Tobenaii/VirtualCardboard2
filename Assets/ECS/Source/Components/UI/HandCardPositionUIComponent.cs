using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class HandCardPositionUI : IComponentData, ISmoothDamp<Vector2>
{
    public RectTransform Transform { get; set; }
    public UIEvents CardEvents { get; set; }
    public float Radius { get; set; }
    public float CircularOffset { get; set; }
    public float HorizontalOffset { get; set; }
    public float RotationalOffset { get; set; }
    public float SmoothTime { get; set; }
    public float HoverOffset { get; set; }
    private Vector2 _velocity;

    public Vector2 SmoothDamp(Vector2 current, Vector2 target)
    {
        return Vector2.SmoothDamp(current, target, ref _velocity, SmoothTime);
    }
}

[MovedFrom(true, sourceClassName: "HandCardUIComponent")]
public class HandCardPositionUIComponent : ComponentAuthoringBase
{
    [SerializeField] private RectTransform _transform;
    [SerializeField] private UIEvents _cardEvents;
    [SerializeField] private float _hoverOffset;
    [SerializeField] private float _radius = 0.06f;
    [SerializeField] private float _circularOffset = 4.7f;
    [SerializeField] private float _horizontalOffset = 0.61f;
    [SerializeField] private float _rotationalOffset = -1.92f;
    [SerializeField] private float _smoothTime = 0.05f;

    public override void AuthorComponent(Entity entity, EntityManager dstManager)
    {
        dstManager.AddComponentData(entity, new HandCardPositionUI()
        {
            Transform = _transform,
            Radius = _radius,
            CircularOffset = _circularOffset,
            HorizontalOffset = _horizontalOffset,
            RotationalOffset = _rotationalOffset,
            SmoothTime = _smoothTime,
            CardEvents = _cardEvents,
            HoverOffset = _hoverOffset
        });
    }
}
