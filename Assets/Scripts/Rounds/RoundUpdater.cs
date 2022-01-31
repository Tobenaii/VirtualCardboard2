using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundUpdater : MonoBehaviour, RoundPhase.ICompletedCallbaks
{
    [SerializeField] private List<RoundPhase> _phases;
    [SerializeField] private RoundPhaseReference _currentPhaseReference;
    private int _currentPhase;
    private RoundPhase currentPhase => _phases[_currentPhase];

    public void NextPhase()
    {
        _currentPhase++;
        _currentPhase = (int)Mathf.Repeat(_currentPhase, _phases.Count);
        currentPhase.Start();
        _currentPhaseReference.SetReference(currentPhase);
    }

    private void Awake()
    {
        _currentPhaseReference.SetReference(currentPhase);
        foreach (var phase in _phases)
            phase.Register(this);
    }

    private void Start()
    {
        currentPhase.Start();
    }

    private void Update()
    {
        currentPhase.Update();
        if (Input.GetKeyDown(KeyCode.Space))
            NextPhase();
    }

    public void RoundPhaseCompleted(RoundPhase phase)
    {
        if (currentPhase == phase)
            NextPhase();
    }
}
