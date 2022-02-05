using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityInstance))]
public class CardTester : MonoBehaviour
{
    [SerializeField] private Card card;
    private EntityInstance _instance;
    private void Awake()
    {
        _instance = GetComponent<EntityInstance>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            card.Execute(_instance);
    }
}
