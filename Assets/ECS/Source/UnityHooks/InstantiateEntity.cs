using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateEntity : MonoBehaviour
{
    [SerializeField] private ModEntity _entity;
    [SerializeField] private bool _onStart;

    private void Start()
    {
        if (_onStart)
            _entity.Instantiate();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            _entity.Instantiate();
    }
}
