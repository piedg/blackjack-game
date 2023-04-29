using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<Card> currentCards = new List<Card>();
    [SerializeField] private Transform hand;

    [SerializeField] protected int currentPoints;

    [SerializeField] protected bool isBusted = false;
    public bool IsBusted { get { return isBusted; } set { isBusted = value; } }

    [SerializeField] protected bool isWaitingCard = false;
    public bool IsWaitingCard { get { return isWaitingCard; } set { isWaitingCard = value; } }

    protected void AttachCard(Card card)
    {
        if (!IsWaitingCard) return;

        card.IsAttached = true;
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

    public int GetPoints()
    {
        return currentPoints;
    }
}

