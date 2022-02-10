using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TMPro.TextMeshProUGUI _title;
    [SerializeField] private TMPro.TextMeshProUGUI _description;
    [SerializeField] private Action _playCardAction;

    public bool IsHovering { get; private set; }
    public bool IsDead { get; private set; }

    private Entity _card;
    private Entity _player;

    private float _upOffset;
    private Vector3 _scaleOffset;
    private Vector3 _velocity;
    private Vector3 _scaleVelocity;

    private Vector3 _prevScale;
    private Vector3 _initScale;

    private void Awake()
    {
        _initScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsHovering = true;
        _upOffset = 50;
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
        var alpha = GetComponent<CanvasGroup>();
        IsDead = true;
        float time = 0;
        while (alpha.alpha > 0)
        {
            alpha.alpha = Mathf.Lerp(1, 0, time / 0.25f);
            time += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    public void RegisterCard(IPrefabCollection cardHand, Entity player)
    {
        var card = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<CardData>(cardHand.Entity);
        _card = cardHand.Entity;
        _player = player;
        _title.text = card.name.ConvertToString();
        _description.text = card.description.ConvertToString();
        gameObject.SetActive(true);
    }

    public void UpdatePosition(float index, Vector3 origin, float circularOffset, float radius, float horizontalOffset, float rotationalOffset, float smoothing)
    {
        var offset = new Vector3(Mathf.Sin(index * (Mathf.Deg2Rad * circularOffset)), Mathf.Cos(index * (Mathf.Deg2Rad * circularOffset)), 0) * radius;
        var targetPos = origin + offset;
        targetPos += Vector3.right * horizontalOffset * index;
        targetPos += Vector3.up * _upOffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _velocity, smoothing);
        transform.rotation = Quaternion.identity;
        transform.Rotate(Vector3.forward, index * -rotationalOffset);
        transform.localScale = Vector3.SmoothDamp(transform.localScale, _initScale + _scaleOffset, ref _scaleVelocity, smoothing);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var actionData = new PlayCard() { Dealer = _player, Card = _card };
        _playCardAction.Execute(actionData);
        StartCoroutine(Die());
    }
}
