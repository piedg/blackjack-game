using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : MonoBehaviour
{
    Vector3 mousePos;
    public GameObject currentCardInstance { get; set; }
    bool hasCard;

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
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (currentCardInstance != null)
        {
            if (Input.GetMouseButton(0))
            {
                hasCard = true;
                currentCardInstance.GetComponent<Card>().IsDragged = true;
                currentCardInstance.transform.position = new Vector3(mousePos.x, 0F, mousePos.y);
            }

            if (Input.GetMouseButtonUp(0))
            {
                hasCard = false;
                currentCardInstance.GetComponent<Card>().IsDragged = false;
                currentCardInstance = null;
            }
        }
        else
            hasCard = false;

        Debug.Log("HasCard?? " + hasCard);
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
}
