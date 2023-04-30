using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Player
{
    [SerializeField] int pointsToStay;

    private void Start()
    {
        pointsToStay = GetRandomNum();

        currentState = ePlayerState.Hit;
    }

    private void Update()
    {
        if (currentPoints > 21)
        {
            UpdateState(ePlayerState.Busted);
        }
        else if (currentPoints == 21)
        {
            UpdateState(ePlayerState.BlackJack);
        }
        else if (currentPoints < pointsToStay)
        {
            UpdateState(ePlayerState.Hit);
        }
        else
        {
            UpdateState(ePlayerState.Stop);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Card card))
        {
            if (!card.IsDragged && currentState == ePlayerState.Hit)
            {
                AttachCard(card);
            }
        }
    }

    public int GetRandomNum()
    {
        return Random.Range(12, 18);
    }
}
