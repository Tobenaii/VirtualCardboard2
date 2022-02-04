using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Player")]
public class Player : ScriptableObject, ISerializationCallbackReceiver
{
    public interface IPlayerCallbacks
    {
        public void OnCardAddedToHand(Card card);
        public void OnCardRemovedFromHand(Card card, int index);
    }

    public void DrawCards(int cards)
    {
        for (int i = 0; i < cards; i++)
        {
            Hand.Add(_deck.PopCard());
            foreach (var callback in _playerCallbacks)
                callback.OnCardAddedToHand(Hand.Last());
        }
            
    }

    [SerializeField] private CardDeck _deck;
    public List<Card> Hand { get; private set; } = new List<Card>();
    public List<IPlayerCallbacks> _playerCallbacks = new List<IPlayerCallbacks>();

    public void Register(IPlayerCallbacks callback)
    {
        _playerCallbacks.Add(callback);
    }

    public void ClearHand()
    {
        foreach (var callback in _playerCallbacks)
        {
            for (int i = 0; i < Hand.Count; i++)
            {
                callback.OnCardRemovedFromHand(Hand[i], i);
            }
        }
        Hand.Clear();
    }

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        Hand.Clear();
    }
}
