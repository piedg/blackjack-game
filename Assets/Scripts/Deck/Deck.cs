using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] List<Card> cards = new List<Card>();
    [SerializeField] List<Card> deck = new List<Card>();

    public static Deck Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        SortCards(cards);
        InitializeDeck(cards);
    }

    public void InitializeDeck(List<Card> cards)
    {
        deck.Clear();

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        float cardsGap = 0; // distance between cards

        foreach (Card card in cards)
        {
            Transform cardInstance = Instantiate(card.GetData().GetTransform(), new Vector3(transform.position.x, transform.position.y + cardsGap, transform.position.z), Quaternion.Euler(90, 0, 0));

            cardInstance.SetParent(transform);
            cardsGap += 0.0005f;

            deck.Add(card);
        }
    }

    public void Shuffle()
    {
        for (int i = 0; i < cards.Count / 2; i++)
        {
            int randomIndex = Random.Range(i, cards.Count);
            int tempIndex = Random.Range(i, cards.Count);

            Card tempCard = cards[randomIndex];
            cards[randomIndex] = cards[tempIndex];
            cards[tempIndex] = tempCard;
        }

        InitializeDeck(cards);
    }

    private void SortCards(List<Card> cards)
    {
        // bubble sort algorithm
        bool swapped;

        for (int i = 0; i < cards.Count - 1; i++)
        {
            swapped = false;
            for (int j = 0; j < cards.Count - i - 1; j++)
            {
                if (cards[j].GetData().GetId() > cards[j + 1].GetData().GetId())
                {
                    Card tempCard = cards[j];
                    cards[j] = cards[j + 1];
                    cards[j + 1] = tempCard;

                    swapped = true;
                }
            }
            if (!swapped)
            {
                break;
            }
        }
    }
}
