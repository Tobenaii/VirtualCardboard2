using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class PlayerHandUI : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _circularOffset;
    [SerializeField] private float _horizontalOffset;
    [SerializeField] private float _rotationalOffset;

    private void Update()
    {
        var baseOffsetCount = (transform.childCount - 1) / 2.0f;
        for (int i = 0; i < transform.childCount; i++)
        {
            var card = transform.GetChild(i);
            var index = i - baseOffsetCount;
            var offset = new Vector3(Mathf.Sin(index * (Mathf.Deg2Rad * _circularOffset)), Mathf.Cos(index * (Mathf.Deg2Rad * _circularOffset)), 0) * _radius;
            var targetPos = transform.position + offset;
            targetPos += Vector3.right * _horizontalOffset * index;
            card.transform.position = targetPos;
            card.transform.rotation = Quaternion.identity;
            card.transform.Rotate(Vector3.forward, index * -_rotationalOffset);
        }
    }
}
