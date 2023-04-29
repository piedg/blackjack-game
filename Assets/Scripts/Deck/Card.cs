using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] CardSO cardSO;

    Bot playerDetected;
    public bool IsDragged;

    public bool isFaceUp = false;

    // Drag and Drop 
    Vector3 startPosition;

    Vector3 dist;
    Vector3 dragStartPosition;
    float posX;
    float posZ;
    float posY;

    float flipRotationSpeed = 10.0f;

    private void Start()
    {
        transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);

        startPosition = transform.position;
    }

    private void Update()
    {
        if (!IsDragged)
        {
            transform.position = Vector3.Lerp(transform.position, startPosition, Time.deltaTime * 5f);
        }

        if (isFaceUp)
        {
            Flip();
        }
    }

    /* void OnMouseDown()
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
         if (playerDetected && playerDetected.IsWaitingCard)
         {
             isFaceUp = true;
             playerDetected.AttachCard(this);
         }
     }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.TryGetComponent(out Bot bot))
        {
            if (!bot.IsWaitingCard) return;

            Debug.Log("Player detected: " + other.transform.parent);
            bot.AttachCard(this);
            playerDetected = bot;
        }
    }

    private void OnTriggerExit(Collider other)
    {
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

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-targetRotation, 0f, 0f), flipRotationSpeed * Time.deltaTime);
    }

    public CardSO GetData()
    {
        return cardSO;
    }
}
