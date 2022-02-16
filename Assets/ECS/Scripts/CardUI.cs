using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IComponentListener<IStatusMessage>
{
    //[SerializeField] private ComponentEvent<IStatusMessage, IStatusMessage.StatusType> _actionError;
    [SerializeField] private TMPro.TextMeshProUGUI _title;
    [SerializeField] private TMPro.TextMeshProUGUI _description;
    [SerializeField] private Action _playCardAction;

    public bool IsHovering { get; private set; }
    public bool IsDead { get; private set; }

    private Entity _card;
    private Entity _player;

    private float _upOffset;
    private Vector3 _scaleOffset;
    private Vector2 _velocity;
    private Vector3 _scaleVelocity;

    private Vector3 _prevScale;
    private Vector3 _initScale;
    private float _waitTimer;

    private void Awake()
    {
        _initScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsHovering = true;
        _upOffset = -0.1f;
        _scaleOffset = new Vector3(0.2f, 0.2f, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsHovering = false;
        _upOffset = 0;
        _scaleOffset = Vector3.zero;
    }

    public IEnumerator Die()
    {
        yield return null;
        var alpha = GetComponent<CanvasGroup>();
        IsDead = true;
        float time = 0;
        while (alpha.alpha > 0)
        {
            alpha.alpha = Mathf.Lerp(1, 0, time / 0.25f);
            time += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
        alpha.alpha = 1;
    }

    public void RegisterCard(IPrefabCollection cardHand, Entity player)
    {
        var card = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<CardData>(cardHand.Entity);
        _card = cardHand.Entity;
        _player = player;
        _title.text = card.name.ConvertToString();
        _description.text = card.description.ConvertToString();
        gameObject.SetActive(true);
        _waitTimer = 0;
    }

    public void UpdatePosition(int index, float cardIndex, float circularOffset, float radius, float horizontalOffset, float rotationalOffset, float smoothing)
    {
        if (_waitTimer < 0.15f * index)
        {
            _waitTimer += Time.deltaTime;
            return;
        }
        var offset = new Vector2(Mathf.Sin(cardIndex * (Mathf.Deg2Rad * circularOffset)), Mathf.Cos(cardIndex * (Mathf.Deg2Rad * circularOffset))) * radius;
        var targetPos = offset;
        targetPos += Vector2.right * horizontalOffset * cardIndex;
        targetPos += Vector2.up * _upOffset;
        var rect = (transform as RectTransform);
        rect.pivot = Vector2.SmoothDamp(rect.pivot, targetPos, ref _velocity, smoothing);
        transform.rotation = Quaternion.identity;
        transform.Rotate(Vector3.forward, cardIndex * -rotationalOffset);
        transform.localScale = Vector3.SmoothDamp(transform.localScale, _initScale + _scaleOffset, ref _scaleVelocity, smoothing);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var actionData = new PlayCard() { Dealer = _player, Card = _card };
        //var entity = _playCardAction.Execute(actionData);
        //_actionError.Register(entity, this);
    }

    public void OnComponentChanged(IStatusMessage value)
    {
        StopAllCoroutines();
        if (value.Status == IStatusMessage.StatusType.Success)
        {
            StartCoroutine(Die());
        }
        else
        {
            var error = value.Message;
            string message = error.ConvertToString();
            StartCoroutine(ErrorText(message));
        }
    }

    private IEnumerator ErrorText(string error)
    {
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
