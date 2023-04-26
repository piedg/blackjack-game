using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] List<CardSO> cards = new List<CardSO>();
    [SerializeField] Queue<CardSO> deck = new Queue<CardSO>();

    private void Start()
    {
        SortCards();

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

        float cardsGap = 0; // distance between cards

        for (int i = 0; i < cards.Count; i++)
        {
            deck.Enqueue(cards[i]);

            Transform cardInstance = Instantiate(cards[i].GetTransform(), new Vector3(0, transform.position.y + cardsGap, 0), Quaternion.Euler(90, 0, 0));

            cardInstance.SetParent(transform);

            cardsGap += 0.0005f;
        }
    }

    private void SortCards()
    {
        // Bubble sort algorithms
        bool swapped;

        for (int i = 0; i < cards.Count - 1; i++)
        {
            swapped = false;
            for (int j = 0; j < cards.Count - i - 1; j++)
            {
                if (cards[j].GetId() > cards[j + 1].GetId())
                {
                    CardSO temp = cards[j];
                    cards[j] = cards[j + 1];
                    cards[j + 1] = temp;
                    swapped = true;
                }
            }
            if (!swapped)
            {
                break;
            }
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
