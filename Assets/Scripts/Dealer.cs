using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : MonoBehaviour
{
    [SerializeField] List<Card> currentCards = new List<Card>();
    [SerializeField] Transform hand;

    public GameObject currentCardInstance { get; set; }
    bool hasCard;
    public bool IsBusted;
    public bool IsWaitingCard;
    int currentPoints;


    public static Dealer Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (currentCardInstance != null)
        {
            if (Input.GetMouseButton(0))
            {
                hasCard = true;
                //currentCardInstance.GetComponent<Card>().IsDragged = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                hasCard = false;
               // currentCardInstance.GetComponent<Card>().IsDragged = false;
                currentCardInstance = null;
            }
        }
        else
            hasCard = false;
    }

    public void OnDeckClick(Card card)
    {
        if (hasCard) return;
        
        currentCardInstance = card.gameObject;
        Debug.Log("Current Card Instance: " + currentCardInstance);
    }

    public void AttachToPlayerHand(Vector3 attachPosition)
    {
        if (currentCardInstance != null)
        {
            currentCardInstance.transform.position = attachPosition;
            hasCard = false;
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
    }
}
