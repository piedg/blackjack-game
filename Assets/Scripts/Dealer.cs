using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : Player
{
    [SerializeField] Card selectedCard;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DrawCard();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (selectedCard)
            {
                ReleaseSelectedCard();
            }
        }

        if (selectedCard)
        {
            DragSelectedCard();
        }
    }

    private void ReleaseSelectedCard()
    {
        selectedCard.IsDragged = false;
        selectedCard = null;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Card card))
        {
            if (!card.IsDragged && GameManager.Instance.dealerTurn)
            {
                Debug.Log("Attach to Dealer");
                AttachCard(card);
            }
        }
    }

    private void DrawCard()
    {
        if (!selectedCard)
        {
            RaycastHit hit = CastRay();

            if (hit.collider.TryGetComponent(out Card card))
            {
                if (!card.IsAttached)
                {
                    selectedCard = card;
                    selectedCard.IsDragged = true;
                }
            }
        }
    }
    private void DragSelectedCard()
    {
        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedCard.gameObject.transform.position).z);

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);

        selectedCard.gameObject.transform.position = new Vector3(
            worldPosition.x,
            selectedCard.gameObject.transform.position.y,
            worldPosition.z);
    }

    private RaycastHit CastRay()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        Physics.Raycast(ray, out hit, 100);

        return hit;
    }
}
