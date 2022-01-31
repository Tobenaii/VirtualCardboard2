using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShittyBattleTest : MonoBehaviour, RoundPhase.ICallbacks
{
    [SerializeField] private Player _player;
    [SerializeField] private RoundPhase _phase;

    private void Awake()
    {
        _phase.Register(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            _phase.Complete();
    }

    public void PhaseEnd()
    {
        _player.ClearHand();
    }

    public void PhaseStart()
    {
    }

    public void PhaseUpdate()
    {
    }
}
