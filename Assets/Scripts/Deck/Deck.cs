using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
   [SerializeField] List<CardSO> cards = new List<CardSO>();

    private void Start()
    {
        CardSO card = DrawCard();
        Debug.Log($"Drawed {card} and its value is {card.GetValue()}");
    }

    public CardSO DrawCard()
    {
        return cards[Random.Range(0, GetCardsNum())];
    }

    public int GetCardsNum()
    {
        return cards.Count;
    }
}
