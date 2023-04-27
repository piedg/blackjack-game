using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] Transform hand;
    [SerializeField] List<Card> currentCards = new List<Card>();

    public bool IsBusted;
    public bool IsWaitingCard;

    int currentPoints;

    private void Update()
    {
        Debug.Log(gameObject.name + " IsWaitingCard? " + IsWaitingCard);

        if (currentPoints > 21)
        {
            IsBusted = true;
        }
    }

    public void AttachCard(Card card)
    {
        if (!IsWaitingCard) return;
        IsWaitingCard = false;
        card.transform.position = hand.position;

        if (!currentCards.Contains(card))
        {
            currentCards.Add(card);
            currentPoints += card.GetData().GetValue();
        }

        CalculateCurrentPoints();
    }

    public void CalculateCurrentPoints()
    {
        Debug.Log(gameObject.name + " current Points: " + currentPoints);
    }

    public void DiscardCards(List<Card> cards)
    {
        cards.Clear();
        currentPoints = 0;
    }
}
