using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "VC2/Card Deck")]
public class CardDeck : ScriptableObject, ISerializationCallbackReceiver
{
    [field: SerializeField] public List<Card> Cards { get; private set; }
    private Stack<Card> _runtimeCards = new Stack<Card>();

    public Card PopCard() => _runtimeCards.Pop();

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        var shuffledCards = Shuffle(new List<Card>(Cards));
        _runtimeCards = new Stack<Card>(shuffledCards);
    }

    public List<Card> Shuffle(List<Card> cards)
    {
        var random = new System.Random();
        int cardCount = cards.Count;
        for (int i = 0; i < (cardCount - 1); i++)
        {
            int r = i + random.Next(cardCount - i);
            Card t = cards[r];
            cards[r] = cards[i];
            cards[i] = t;
        }
        return cards;
    }
}
