using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Player
{
    [SerializeField] int pointsToStay;

    private void Start()
    {
        pointsToStay = GetRandomNum();

        currentState = eState.Hit;
    }

    private void Update()
    {
        if (currentPoints > 21)
        {
            UpdateState(eState.Busted);
        }
        else if (currentPoints == 21)
        {
            UpdateState(eState.BlackJack);
        }
        else if (currentPoints < pointsToStay)
        {
            UpdateState(eState.Hit);
        }
        else
        {
            UpdateState(eState.Stop);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Card card))
        {
            if (!card.IsDragged && currentState == eState.Hit)
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
