using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] List<Card> cards = new List<Card>();
    [SerializeField] List<Card> deck = new List<Card>();

    public static Deck Instance;

    private void Awake()
    {
        if (Instance == null)
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
        InstantiateCards(deck);
    }

    public void InitializeDeck(List<Card> cards)
    {
        deck.Clear();

        foreach (Card card in cards)
        {
            deck.Add(card);
        }
    }

    // Called by UI Button
    public void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count / 2; i++)
        {
            int randomIndex = Random.Range(i, deck.Count);
            int tempIndex = Random.Range(i, deck.Count);

            Card tempCard = deck[randomIndex];
            deck[randomIndex] = deck[tempIndex];
            deck[tempIndex] = tempCard;
        }

        InstantiateCards(deck);
    }

    private void InstantiateCards(List<Card> cardsList)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        float cardsGap = 0; // distance between cards

        foreach (Card card in cardsList)
        {
            Transform cardInstance = Instantiate(card.GetData().GetTransform(), new Vector3(transform.position.x, transform.position.y + cardsGap, transform.position.z), Quaternion.Euler(90, 0, 0));

            cardInstance.SetParent(transform);
            cardsGap += 0.0005f;
        }
    }

    // TODO implement for update deck
    public void RemoveCardFromDeck(Card card)
    {
        Debug.Log("Ho rimosso " + card.name);
        deck.Remove(card);
    }

    // TODO implement for update deck when is next turn
    public void AddCardToDeck(Card card)
    {
        Debug.Log("Add card " + card.name);
        deck.Add(card);
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
