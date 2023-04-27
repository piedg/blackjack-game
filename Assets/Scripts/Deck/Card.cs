using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] CardSO cardSO;
    public bool IsDragged;
    public bool IsDraggable;

    public bool isFaceUp = false;

    Vector3 startPosition;

    Vector3 dist;
    Vector3 dragStartPosition;
    float posX;
    float posZ;
    float posY;

    bool playerNotFound;

    public float rotationSpeed = 10.0f;
    
    private void Start()
    {
        startPosition = transform.position;
        IsDragged = false;
        IsDraggable = false;
    }

    private void Update()
    {
        // Attach to player hand
        // TODO

        // Drag is released
        if (!IsDragged)
        {
            transform.position = Vector3.Lerp(transform.position, startPosition, Time.deltaTime * 5f);
        }

        if(isFaceUp)
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
        // playerNotFound = !TryFindPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Bot>())
        {
            Deck.Instance.RemoveCardFromDeck(this);
            Debug.Log("Player found: " + other.name);
            IsDragged = true;
            isFaceUp = true;
        }
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

    private bool TryFindPlayer()
    {
        return false;
    }
}
