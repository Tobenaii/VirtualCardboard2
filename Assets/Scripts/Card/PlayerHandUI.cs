using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class PlayerHandUI : MonoBehaviour, Player.IPlayerCallbacks
{
    [SerializeField] private Player _player;
    [SerializeField] private CardAuthoring _cardAuthoring;
    [SerializeField] private float _cardSize;
    [SerializeField] private float _radius;
    [SerializeField] private float _circularOffset;
    [SerializeField] private float _horizontalOffset;
    [SerializeField] private float _rotationalOffset;
    [SerializeField] private float _drawSmoothing;

    private Stack<CardAuthoring> _cardPool = new Stack<CardAuthoring>();

    private Dictionary<Transform, Vector3> _drawVelocity = new Dictionary<Transform, Vector3>();


    private void Awake()
    {
        _player.Register(this);

        for (int i = 0; i < 20; i++)
        {
            var card = Instantiate(_cardAuthoring, transform);
            card.gameObject.SetActive(false);
            _cardPool.Push(card);
        }
    }

    private void Update()
    {
        int activeCount = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            var card = transform.GetChild(i);
            if (card.gameObject.activeSelf)
                activeCount++;
        }
        var baseOffsetCount = (activeCount - 1) / 2.0f;
        int done = 0;
        int activeIndex = 0;



        for (int i = 0; i < transform.childCount; i++)
        {
            var card = transform.GetChild(i);
            if (!card.gameObject.activeSelf)
                continue;
            var index = activeIndex - baseOffsetCount;
            activeIndex++;
            var offset = new Vector3(Mathf.Sin(index * (Mathf.Deg2Rad * _circularOffset)), Mathf.Cos(index * (Mathf.Deg2Rad * _circularOffset)), 0) * _radius;
            var targetPos = transform.position + offset;
            targetPos += transform.right * _horizontalOffset * index;
            if (Application.isPlaying && _drawVelocity.ContainsKey(card))
                card.transform.position = SmoothDamp(card.transform.position, targetPos, card, _drawSmoothing, Time.deltaTime);
            else
                card.transform.position = targetPos;
            if ((targetPos - card.transform.position).sqrMagnitude < 1.0f)
                done++;
            card.transform.localRotation = Quaternion.identity;
            card.transform.Rotate(transform.forward, index * -_rotationalOffset);
            card.localScale = new Vector3(_cardSize, _cardSize, _cardSize);
        }
    }

    public void OnCardAddedToHand(Card card)
    {
        var cardAuthoring = _cardPool.Pop();
        cardAuthoring.gameObject.SetActive(true);
        cardAuthoring.AssignCard(card);
        _drawVelocity.Add(cardAuthoring.transform, Vector3.zero);
    }

    public void OnCardRemovedFromHand(Card card, int index)
    {
        Destroy(transform.GetChild(index).gameObject);
    }

    public Vector3 SmoothDamp(Vector3 current, Vector3 target, Transform transform, float smoothTime, float deltaTime, float maxSpeed = float.PositiveInfinity)
    {
        Vector3 currentVelocity = _drawVelocity[transform];
        float num = 0f;
        float num2 = 0f;
        float num3 = 0f;
        smoothTime = Mathf.Max(0.0001f, smoothTime);
        float num4 = 2f / smoothTime;
        float num5 = num4 * deltaTime;
        float num6 = 1f / (1f + num5 + 0.48f * num5 * num5 + 0.235f * num5 * num5 * num5);
        float num7 = current.x - target.x;
        float num8 = current.y - target.y;
        float num9 = current.z - target.z;
        Vector3 vector = target;
        float num10 = maxSpeed * smoothTime;
        float num11 = num10 * num10;
        float num12 = num7 * num7 + num8 * num8 + num9 * num9;
        if (num12 > num11)
        {
            float num13 = (float)Math.Sqrt(num12);
            num7 = num7 / num13 * num10;
            num8 = num8 / num13 * num10;
            num9 = num9 / num13 * num10;
        }

        target.x = current.x - num7;
        target.y = current.y - num8;
        target.z = current.z - num9;
        float num14 = (currentVelocity.x + num4 * num7) * deltaTime;
        float num15 = (currentVelocity.y + num4 * num8) * deltaTime;
        float num16 = (currentVelocity.z + num4 * num9) * deltaTime;
        currentVelocity.x = (currentVelocity.x - num4 * num14) * num6;
        currentVelocity.y = (currentVelocity.y - num4 * num15) * num6;
        currentVelocity.z = (currentVelocity.z - num4 * num16) * num6;
        num = target.x + (num7 + num14) * num6;
        num2 = target.y + (num8 + num15) * num6;
        num3 = target.z + (num9 + num16) * num6;
        float num17 = vector.x - current.x;
        float num18 = vector.y - current.y;
        float num19 = vector.z - current.z;
        float num20 = num - vector.x;
        float num21 = num2 - vector.y;
        float num22 = num3 - vector.z;
        if (num17 * num20 + num18 * num21 + num19 * num22 > 0f)
        {
            num = vector.x;
            num2 = vector.y;
            num3 = vector.z;
            currentVelocity.x = (num - vector.x) / deltaTime;
            currentVelocity.y = (num2 - vector.y) / deltaTime;
            currentVelocity.z = (num3 - vector.z) / deltaTime;
        }

        _drawVelocity[transform] = currentVelocity;
        return new Vector3(num, num2, num3);
    }
}
