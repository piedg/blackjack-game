using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] CardSO cardSO;
    public bool IsDraggable;

    Bot playerDetected;

    public bool isFaceUp = false;

    Vector3 startPosition;

    Vector3 dist;
    Vector3 dragStartPosition;
    float posX;
    float posZ;
    float posY;

    bool playerHandFound;

    public float rotationSpeed = 10.0f;

    private void Start()
    {
        transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);

        startPosition = transform.position;
        IsDraggable = true;
    }

    private void Update()
    {
        // Drag is released
        if (!playerHandFound)
        {
            transform.position = Vector3.Lerp(transform.position, startPosition, Time.deltaTime * 5f);
        }

        if (isFaceUp)
        {
            Flip();
        }
    }

    void OnMouseDown()
    {
        if (!IsDraggable) return;

        dragStartPosition = transform.position;

        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
        posZ = Input.mousePosition.z - dist.z;
    }

    void OnMouseDrag()
    {
        if (!IsDraggable) return;

        float disX = Input.mousePosition.x - posX;
        float disY = Input.mousePosition.y - posY;
        float disZ = Input.mousePosition.z - posZ;

        Vector3 lastPos = Camera.main.ScreenToWorldPoint(new Vector3(disX, disY, disZ));

        transform.position = new Vector3(lastPos.x, dragStartPosition.y, lastPos.z);
    }

    private void OnMouseUp()
    {
        if (playerHandFound && playerDetected && playerDetected.IsWaitingCard)
        {
            isFaceUp = true;
            SetIsDraggable(false);
            playerDetected.AttachCard(this);
            Deck.Instance.RemoveCardFromDeck(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.TryGetComponent(out Bot bot))
        {
            if (!bot.IsWaitingCard) return;

            Debug.Log("Player detected: " + other.transform.parent);
            playerHandFound = true;
            playerDetected = bot;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerHandFound = false;
        playerDetected = null;
    }

    public void Flip()
    {
        float currentRotation = transform.rotation.x;
        float targetRotation = currentRotation + 90.0f;

        if (targetRotation < 0.0f)
        {
            targetRotation += 360.0f;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-targetRotation, 0f, 0f), rotationSpeed * Time.deltaTime);
    }

    public CardSO GetData()
    {
        return cardSO;
    }

    public void SetIsDraggable(bool value)
    {
        IsDraggable = value;
    }
}
