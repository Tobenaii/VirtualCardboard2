using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class CardUIElements
{
    [field: SerializeField] public TextMeshProUGUI NameText { get; private set; }
    [field: SerializeField] public Image ResourceImage { get; private set; }
    [field: SerializeField] public Image MainImage { get; private set; }
    [field: SerializeField] public TextMeshProUGUI TypeText { get; private set; }
    [field: SerializeField] public TextMeshProUGUI DescriptionText { get; private set; }
}

public class CardAuthoring : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Card _card;
    [SerializeField] private CardUIElements _uiElements;
    private float _zTarget;

    public void AssignCard(Card card)
    {
        _card = card;
        UpdateUI();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    private void UpdateUI()
    {
        if (_card == null)
        {
            _uiElements.NameText.text = "Name";
            _uiElements.ResourceImage.sprite = null;
            _uiElements.MainImage.sprite = null;
            _uiElements.TypeText.text = "Type";
            _uiElements.DescriptionText.text = "Description";
            return;
        }
        _uiElements.NameText.text = _card.Name;
        _uiElements.ResourceImage.sprite = _card.Resource.Icon;
        _uiElements.MainImage.sprite = _card.Image;
        _uiElements.TypeText.text = _card.Type;
        _uiElements.DescriptionText.text = _card.Description;
    }

    private void OnValidate()
    {
        UpdateUI();
    }
}
