using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Round Phase")]
public class RoundPhase : ScriptableObject
{
    public interface ICallbacks
    {
        public void PhaseStart();
        public void PhaseUpdate();
        public void PhaseEnd();
    }

    public interface ICompletedCallbaks
    {
        public void RoundPhaseCompleted(RoundPhase phase);
    }

    [field: SerializeField] public string Name { get; private set; }

    private List<ICallbacks> _callbacks = new List<ICallbacks>();
    private List<ICompletedCallbaks> _completedCallbacks = new List<ICompletedCallbaks>();

    public void Register(ICallbacks listener)
    {
        _callbacks.Add(listener);
    }

    public void Register(ICompletedCallbaks listener)
    {
        _completedCallbacks.Add(listener);
    }

    public void Start()
    {
        foreach (ICallbacks callback in _callbacks)
            callback.PhaseStart();
    }

    public void Update()
    {
        foreach (ICallbacks callback in _callbacks)
            callback.PhaseUpdate();
    }

    public void Complete()
    {
        foreach (ICallbacks callback in _callbacks)
            callback.PhaseEnd();
        foreach (ICompletedCallbaks callback in _completedCallbacks)
            callback.RoundPhaseCompleted(this);
    }
}
