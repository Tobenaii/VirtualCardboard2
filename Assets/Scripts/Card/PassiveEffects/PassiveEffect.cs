using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Passive Effect")]
public class PassiveEffect : ScriptableObject
{
    private struct PassiveEffectRuntime
    {
        private float duration;
        private float amount;
        private float tickRate;
        private EntityInstance dealer;
        private EntityInstance receiver;
        private int startRound;
        private float tick;


        public PassiveEffectRuntime(EntityInstance dealer, EntityInstance receiver, int startRound, float duration, float amount, float tickRate)
        {
            this.tick = 0;
            this.startRound = startRound;
            this.duration = duration;
            this.amount = amount;
            this.tickRate = tickRate;
            this.dealer = dealer;
            this.receiver = receiver;
        }

        public bool Update(AttributeResolver resolver, int currentRound)
        {
            if (this.startRound + this.duration > currentRound)
                return true;
            if (tick >= tickRate)
            {
                tick -= tickRate;
                resolver.Resolve(dealer, receiver, amount);
            }
            tick += Time.deltaTime;
            return false;
        }
    }

    [SerializeField] private AttributeResolver _attributeResolver;
    [SerializeField] private Entity _matchEntity;
    [SerializeField] private Attribute _currentRound;
    [SerializeField] private float _amount;
    [SerializeField] private float _tickRate;
    private List<PassiveEffectRuntime> _runtimes = new List<PassiveEffectRuntime>();

    public void AddEntity(EntityInstance dealer, EntityInstance receiver, float duration)
    {
        int startRound = (int)_currentRound[_matchEntity[0]];
        _runtimes.Add(new PassiveEffectRuntime(dealer, receiver, startRound, duration, _amount, _tickRate));
    }

    public void Update()
    {
        foreach (var effect in _runtimes)
            effect.Update(_attributeResolver, (int)_currentRound[_matchEntity[0]]);
    }
}
