using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Player
{
    [SerializeField] int pointsToStay;

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
        else if (currentPoints == 21)
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

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Card card))
        {
            if (!card.GetIsDragged() && IsWaitingCard)
            {
                AttachCard(card);
            }
        }
    }

    public int GetRandomNum()
    {
        return Random.Range(11, 18);
    }

  
}
