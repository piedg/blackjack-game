using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] CardSO cardSO;

    Bot playerDetected;
    [SerializeField] bool isDragged;
    [SerializeField] bool isAttached;

    public bool isFaceUp = false;

    Vector3 startPosition;

    float flipRotationSpeed = 10.0f;

    private void Start()
    {
        transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);

        startPosition = transform.position;
    }

    private void Update()
    {
        if (!isDragged)
        {
            if(isAttached)
            {
                Flip();
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, startPosition, Time.deltaTime * 5f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.transform.parent.TryGetComponent(out Bot bot))
        {
            if (!bot.IsWaitingCard) return;

            Debug.Log("Player detected: " + other.transform.parent);
            bot.AttachCard(this);
            //playerDetected = bot;
        } */
    }

    private void OnTriggerExit(Collider other)
    {
       // playerDetected = null;
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

    public void SetIsDragged(bool value)
    {
        isDragged = value;
    }

    public bool GetIsDragged()
    {
        return isDragged;
    }

    public void SetIsAttached(bool value)
    {
        isAttached = value;
    }

    public bool GetIsAttached()
    {
        return isAttached;
    }
}
