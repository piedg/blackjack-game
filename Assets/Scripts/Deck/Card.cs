using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] CardSO cardSO;
    bool isDraggable = false;

    Vector3 startPosition;

    Vector3 dist;
    Vector3 dragStartPosition;
    float posX;
    float posZ;
    float posY;

    bool playerNotFound;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (playerNotFound)
        {
            transform.position = Vector3.Lerp(transform.position, startPosition, Time.deltaTime * 5f);
        }
    }

    void OnMouseDown()
    {
        dragStartPosition = transform.position;

        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
        posZ = Input.mousePosition.z - dist.z;
    }

    void OnMouseDrag()
    {
        float disX = Input.mousePosition.x - posX;
        float disY = Input.mousePosition.y - posY;
        float disZ = Input.mousePosition.z - posZ;

        Vector3 lastPos = Camera.main.ScreenToWorldPoint(new Vector3(disX, disY, disZ));

        transform.position = new Vector3(lastPos.x, dragStartPosition.y, lastPos.z);
    }

    private void OnMouseUp()
    {
        playerNotFound = !TryFindPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInParent<Bot>())
        {
            Debug.Log("Player found: ");
        }
    }

    public CardSO GetData()
    {
        return cardSO;
    }

    public void SetIsDraggable(bool value)
    {
        isDraggable = value;
    }

    public bool GetIsDraggable()
    {
        return isDraggable;
    }

    private bool TryFindPlayer()
    {
        return false;
    }
}
