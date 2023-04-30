using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] CardSO cardSO;

    [SerializeField] bool isDragged;
    public bool IsDragged { get { return isDragged; } set { isDragged = value; } }

    [SerializeField] bool isAttached;
    public bool IsAttached { get { return isAttached; } set { isAttached = value; } }

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
                // return to start position
                transform.position = Vector3.Lerp(transform.position, startPosition, Time.deltaTime * 5f);
            }
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

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-targetRotation, 0f, 0f), flipRotationSpeed * Time.deltaTime);
    }

    public CardSO GetData()
    {
        return cardSO;
    }
}
