using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _title;
    [SerializeField] private TMPro.TextMeshProUGUI _description;
    public string Title
    {
        set
        {
            _title.text = value;
        }
    }

    public string Description
    {
        set
        {
            _description.text = value;
        }
    }
}
