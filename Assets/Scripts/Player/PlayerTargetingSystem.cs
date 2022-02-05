using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingSystem : MonoBehaviour
{
    [SerializeField] private EntityGroup _targetingGroup;
    [SerializeField] private SingleTargetResolver _targetResolver;

    private int _currentTargetIndex;
    private EntityInstance CurrentTarget => _targetingGroup[_currentTargetIndex];

    private void Start()
    {
        _targetResolver.SetTarget(CurrentTarget);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _currentTargetIndex++;
            if (_currentTargetIndex >= _targetingGroup.Count)
                _currentTargetIndex = 0;
            _targetResolver.SetTarget(CurrentTarget);
        }
    }
}
