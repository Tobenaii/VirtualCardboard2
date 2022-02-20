using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InstantiateEntity : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private ModEntity _entity;
    [SerializeField] private bool _onStart;
    [SerializeField] private bool _onClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_onClick)
            _entity.Instantiate();
    }

    private void Start()
    {
        if (_onStart)
            _entity.Instantiate();
    }
}
