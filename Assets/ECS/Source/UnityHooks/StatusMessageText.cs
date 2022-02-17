using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class StatusMessageText : MonoBehaviour, IComponentListener<IActionStatus>
{
    [SerializeField] private ComponentEvent<IActionStatus> _statusEvent;
    [SerializeField] private TMPro.TextMeshProUGUI _text;
    [SerializeField] private float _visibleTime;

    private void Start()
    {
        _statusEvent.Register(this);
    }

    public void OnComponentChanged(IActionStatus value)
    {
        //There should be a None status to check against, since success might also want a message
        if (value.Status == IActionStatus.StatusType.Success) 
            return;
        var message = value.Message;
        StopAllCoroutines();
        StartCoroutine(SetText(message.ConvertToString()));
    }

    private IEnumerator SetText(string text)
    {
        _text.text = text;
        float timer = _visibleTime;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        _text.text = string.Empty;
    }
}
