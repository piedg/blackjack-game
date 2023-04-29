using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : MonoBehaviour
{
    [SerializeField] List<Card> currentCards = new List<Card>();

    public GameObject selectedObject;
    private Card selectedCard;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!selectedCard)
            {
                RaycastHit hit = CastRay();

                if (hit.collider.TryGetComponent(out Card card))
                {
                    selectedCard = card;
                    selectedCard.SetIsDragged(true);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if(selectedCard)
            {
                selectedCard.SetIsDragged(false);
                selectedCard = null;
            }
        }

        if (selectedCard)
        {
            DragSelectedCard();
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
