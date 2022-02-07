using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class TargetMarkerUI : MonoBehaviour, IComponentListener<ISingleTargeting>
{
    [SerializeField] private SingleTargetingEvent _event;
    [SerializeField] private EntityRef _target;
    [SerializeField] private float _smoothing;

    private Vector3 _targetPos;
    private Vector3 _targetScale;

    private Vector3 _posVelocity;
    private Vector3 _scaleVelocity;

    private Transform _camera;

    private void Start()
    {
        _camera = Camera.main.transform;
        _event.Register(_target.Entity, this);
    }

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, _targetPos, ref _posVelocity, _smoothing);
        transform.localScale = Vector3.SmoothDamp(transform.localScale, _targetScale, ref _scaleVelocity, _smoothing);
        float prevZ = transform.eulerAngles.z;
        transform.LookAt(_camera);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, prevZ);
        transform.Rotate(transform.forward, 20 * Time.deltaTime, Space.World);
    }

    public void OnComponentChanged(ISingleTargeting target)
    {
        _targetPos = target.Position;
        _targetScale = new Vector3(target.Scale, target.Scale, target.Scale);
    }
}
