using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] protected List<Card> currentCards = new List<Card>();
    [SerializeField] protected Transform hand;

    [SerializeField] protected int currentPoints;

    public bool IsBusted;
    public bool IsWaitingCard;

    protected void AttachCard(Card card)
    {
        if (!IsWaitingCard) return;

        card.SetIsAttached(true);
        card.transform.SetParent(hand.transform);

        if (!currentCards.Contains(card))
        {
            currentCards.Add(card);
            AddPoints(card.GetData().GetValue());
        }
    }
    protected void AddPoints(int points)
    {
        currentPoints += points;
        Debug.Log(gameObject.name + " CurrentPoints " + currentPoints);
    }
    protected void DiscardCards(List<Card> cards)
    {
        foreach (Card card in cards)
        {
            card.gameObject.SetActive(false);
        }

        cards.Clear();
        currentPoints = 0;
    }

    public int GetPoints()
    {
        return currentPoints;
    }
}

