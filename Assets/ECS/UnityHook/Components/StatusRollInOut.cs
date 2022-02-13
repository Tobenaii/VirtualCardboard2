using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class StatusRollInOut : MonoBehaviour, IComponentListener<IStatusMessage>
{
    [SerializeField] private ComponentEvent<IStatusMessage> _statusEvent;
    [SerializeField] private TMPro.TextMeshProUGUI _text;
    [SerializeField] private float _time;
    [SerializeField] private AnimationCurve _speedCurve;

    private float _timer;

    private void Awake()
    {
        _timer = _time;
        _statusEvent.Register(this);
    }

    private void Update()
    {
        var delta = _timer / _time;
        _timer += Time.deltaTime * _speedCurve.Evaluate(delta);
        if (_timer >= _time)
        {
            _timer = _time;
            _text.gameObject.SetActive(false);
        }
        var rect = (_text.transform as RectTransform);
        var newX = Mathf.Lerp(1.5f, -0.5f, delta);
        rect.pivot = new Vector2(newX, rect.pivot.y);
    }

    public void OnComponentChanged(IStatusMessage value)
    {
        _timer = 0;
        var message = value.Message;
        _text.text = message.ConvertToString();
        _text.gameObject.SetActive(true);
    }
}
