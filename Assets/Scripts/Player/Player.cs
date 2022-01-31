using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Player")]
public class Player : ScriptableObject, ISerializationCallbackReceiver
{
    public interface IPlayerCallbacks
    {
        public void OnCardAddedToHand(int index);
        public void OnCardRemovedFromHand(int index);
    }

    [SerializeField] private CardDeck _deck;
    public List<Card> Hand { get; private set; } = new List<Card>();
    public List<IPlayerCallbacks> _playerCallbacks = new List<IPlayerCallbacks>();

    public void Register(IPlayerCallbacks callback)
    {
        _playerCallbacks.Add(callback);
    }

    public void DrawCard()
    {
        Hand.Add(_deck.PopCard());
        foreach (var callback in _playerCallbacks)
            callback.OnCardAddedToHand(Hand.Count - 1);
    }

    public void ClearHand()
    {
        foreach (var callback in _playerCallbacks)
        {
            for (int i = 0; i < Hand.Count; i++)
            {
                callback.OnCardRemovedFromHand(i);
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
