using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUIList : MonoBehaviour
{
    private List<CardUI> _cards = new List<CardUI>();

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            _cards.Add(child.GetComponent<CardUI>());
        }
    }

    public void SetData(string title, string desc, int index)
    {
        var card = _cards[index];
        card.Title = title;
        card.Description = desc;
        card.gameObject.SetActive(true);
    }
}
