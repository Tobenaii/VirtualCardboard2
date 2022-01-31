using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDrawer : MonoBehaviour, RoundPhase.ICallbacks
{
    [SerializeField] private RoundPhase _roundPhase;
    [SerializeField] private Player _player;

    public void Awake()
    {
        _roundPhase.Register(this);
    }

    public void PhaseStart()
    {
        for (int i = 0; i < 5; i++)
        {
            _player.DrawCard();
        }
    }

    public void PhaseEnd()
    {
    }

    public void PhaseUpdate()
    {

    }
}
