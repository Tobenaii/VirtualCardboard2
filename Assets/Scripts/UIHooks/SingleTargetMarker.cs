using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTargetMarker : MonoBehaviour
{
    [SerializeField] private SingleTargetResolver _targetResolver;
    [SerializeField] private float _switchTime = 0.1f;
    public EntityInstance Target => _targetResolver.Target;

    private Vector3 _positionVelocity;
    private Vector3 _scaleVelocity;
    private Transform _camera;

    private void Awake()
    {
        _camera = Camera.main.transform;
    }

    private void Update()
    {
        Transform markerTarget = Target.TargetingPoint;

        transform.position = Vector3.SmoothDamp(transform.position, markerTarget.position, ref _positionVelocity, _switchTime);
        transform.localScale = Vector3.SmoothDamp(transform.localScale, markerTarget.localScale, ref _scaleVelocity, _switchTime);
        transform.LookAt(_camera);
    }
}
