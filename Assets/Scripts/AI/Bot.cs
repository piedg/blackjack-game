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
    int pointsToStay;

    private void Start()
    {
        pointsToStay = GetRandomNum();
        IsWaitingCard = true;
    }

    private void Update()
    {
        if (currentPoints > 21)
        {
            Debug.Log(gameObject.name + " Busted!");
            IsBusted = true;
            IsWaitingCard = false;
        }
        else if(currentPoints == 21)
        {
            Debug.Log(gameObject.name + " Black Jack!");
            IsWaitingCard = false;
        }
        else if (currentPoints < pointsToStay)
        {
            Debug.Log(gameObject.name + " Hit!");
            IsWaitingCard = true;
        }
        else
        {
            Debug.Log(gameObject.name + " Stop!");
            IsWaitingCard = false;
        }
    }

    public void AttachCard(Card card)
    {
        if (!IsWaitingCard) return;

        //card.transform.position = hand.position;

        if (!currentCards.Contains(card))
        {
            currentCards.Add(card);
        }
        AddPoints(card.GetData().GetValue());
    }

    public int GetRandomNum()
    {
        return Random.Range(11, 15);
    }

    public void AddPoints(int points)
    {
        currentPoints += points;
        Debug.Log(gameObject.name + " CurrentPoints " + currentPoints);
    }

    public void DiscardCards(List<Card> cards)
    {
        foreach (Card card in cards)
        {
            card.gameObject.SetActive(false);
        }

        cards.Clear();
        currentPoints = 0;
    }
}
