using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Player
{
    [SerializeField] int pointsToStay;

    private void Start()
    {
        pointsToStay = GetRandomNum();
    }

    private void Update()
    {
        if (currentPoints > 21)
        {
            Debug.Log(gameObject.name + " Busted!");
            isBusted = true;
            isWaitingCard = false;
        }
        else if (currentPoints == 21)
        {
            Debug.Log(gameObject.name + " Black Jack!");
            isWaitingCard = false;
        }
        else if (currentPoints < pointsToStay)
        {
            Debug.Log(gameObject.name + " Hit!");
            isWaitingCard = true;
        }
        else
        {
            Debug.Log(gameObject.name + " Stop!");
            isWaitingCard = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Card card))
        {
            if (!card.IsDragged && isWaitingCard)
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
