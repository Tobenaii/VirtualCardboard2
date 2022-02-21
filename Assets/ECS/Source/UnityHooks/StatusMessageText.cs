using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class StatusMessageText : MonoBehaviour, IComponentChangedListener<IPerformActions>
{
    [SerializeField] private ComponentEvent<IPerformActions> _statusEvent;
    [SerializeField] private TMPro.TextMeshProUGUI _text;
    [SerializeField] private float _visibleTime;

    private string[] _prepends = new string[1] { "Not Enough " };

    private void Start()
    {
        _statusEvent.Register(this);
    }

    public void OnComponentChanged(IPerformActions value)
    {
        //There should be a None status to check against, since success might also want a message
        if (value.Status == IPerformActions.StatusType.Success) 
            return;
        var prepend = _prepends[(int)value.Failure];
        var message = value.Message;
        StopAllCoroutines();
        StartCoroutine(SetText(prepend + message.ConvertToString()));
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
