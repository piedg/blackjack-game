using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<Card> currentCards = new List<Card>();
    [SerializeField] private Transform hand;

    [SerializeField] protected int currentPoints;

    protected ePlayerState currentState;

    protected void AttachCard(Card card)
    {
        if (currentState != ePlayerState.Hit) return;

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

    protected void UpdateState(ePlayerState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case ePlayerState.Busted:
                Debug.Log(gameObject.name + " Busted!");
                break;
            case ePlayerState.Hit:
                Debug.Log(gameObject.name + " Hit!");
                break;
            case ePlayerState.Stop:
                Debug.Log(gameObject.name + " Stop!");
                break;
            case ePlayerState.BlackJack:
                Debug.Log(gameObject.name + " Black Jack!");
                break;
            default:
                Debug.Log(gameObject.name + " No State!");
                break;
        }
    }

    public ePlayerState GetCurrentState()
    {
        return currentState;
    }
}

public enum ePlayerState
{
    Busted,
    Hit,
    Stop,
    BlackJack
}

