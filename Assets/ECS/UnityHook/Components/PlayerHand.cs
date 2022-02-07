using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerHand : MonoBehaviour, IComponentListener<IPrefabCollection>
{
    [SerializeField] private EntityRef _entity;
    [SerializeField] private CardHandEvent _cardHandEvent;

    [SerializeField] private float _radius;
    [SerializeField] private float _circularOffset;
    [SerializeField] private float _horizontalOffset;
    [SerializeField] private float _rotationalOffset;
    [SerializeField] private float _smoothing;

    private List<CardUI> _cards = new List<CardUI>();

    private void Start()
    {
        if (Application.isPlaying)
            _cardHandEvent.Register(_entity.Entity, this);
    }

    private void Update()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            _cards[i].transform.SetSiblingIndex(i);
            if (_cards[i].IsDead)
            {
                _cards.RemoveAt(i);
                i--;
            }    
        }

        var baseOffsetCount = (_cards.Count - 1) / 2.0f;
        int index = 0;
        foreach (var card in _cards)
        {
            var cardIndex = index - baseOffsetCount;
            card.UpdatePosition(cardIndex, transform.position, _circularOffset, _radius, _horizontalOffset, _rotationalOffset, _smoothing);
            index++;
        }

        index = transform.childCount - 2;
        for (int i = 0; i < _cards.Count; i++)
        {
            if (_cards[i].IsHovering)
            {
                _cards[i].transform.SetAsLastSibling();
                for (int y = i + 1; y < _cards.Count; y++)
                {
                    _cards[y].transform.SetSiblingIndex(index);
                    index--;
                }
                return;
            }
        }
    }

    public void OnComponentChanged(IPrefabCollection newValue)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (child.gameObject.activeSelf)
                continue;
            var card = child.GetComponent<CardUI>();
            card.RegisterCard(newValue, _entity.Entity);
            _cards.Add(card);
            return;
        }
    }
}
