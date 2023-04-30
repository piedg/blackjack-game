using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<Card> currentCards = new List<Card>();
    [SerializeField] private Transform hand;

    [SerializeField] protected int currentPoints;

    protected eState currentState;

    protected void AttachCard(Card card)
    {
        if (currentState != eState.Hit) return;

        card.IsAttached = true;
        card.transform.SetParent(hand.transform);

        if (!currentCards.Contains(card))
        {
            currentCards.Add(card);

            if (currentCards.Count > 2 && card.GetData().IsAce())
            {
                AddPoints(1);
            }
            else
            {
                AddPoints(card.GetData().GetValue());
            }
        }

        Deck.Instance.RemoveCardFromDeck(card);
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

    protected void UpdateState(eState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case eState.Busted:
                Debug.Log(gameObject.name + " Busted!");
                break;
            case eState.Hit:
                Debug.Log(gameObject.name + " Hit!");
                break;
            case eState.Stop:
                Debug.Log(gameObject.name + " Stop!");
                break;
            case eState.BlackJack:
                Debug.Log(gameObject.name + " Black Jack!");
                break;
            default:
                Debug.Log(gameObject.name + " No State!");
                break;
        }
    }

    public eState GetCurrentState()
    {
        return currentState;
    }
}

public enum eState
{
    Busted,
    Hit,
    Stop,
    BlackJack
}

