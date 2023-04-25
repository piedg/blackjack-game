using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] List<CardSO> cards = new List<CardSO>();
    [SerializeField] Queue<CardSO> deck = new Queue<CardSO>();

    private void Start()
    {
        InitializeDeck();
    }

    private void OnMouseDown()
    {
        if (!CanDraw()) return;

        CardSO card = DrawCard();
        Debug.Log($"Drawed {card} and its value is {card.GetValue()}");
        Debug.Log($"Cards in the deck: {deck.Count}");
    }

    public void InitializeDeck()
    {
        deck.Clear();

        float zPos = 0; // Distance between cards

        for (int i = 0; i < cards.Count; i++)
        {
            deck.Enqueue(cards[i]);

            Transform cardInstance = Instantiate(cards[i].GetTransform(), new Vector3(0, transform.position.y + zPos, 0), Quaternion.Euler(90, 0, 0));

            cardInstance.SetParent(transform);

            zPos += 0.0005f;
        }
    }

    public CardSO DrawCard()
    {
        return deck.Dequeue();
    }

    public bool CanDraw()
    {
        if (deck.Count > 0)
            return true;
        else
            return false;
    }
}
