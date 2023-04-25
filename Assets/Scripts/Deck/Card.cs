using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    Vector3 mousePosition;
    Vector3 startingPosition;

    bool playerNotFound;

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        if (playerNotFound)
        {
            transform.position = Vector3.Lerp(transform.position, startingPosition, Time.deltaTime * 5f);
        }
    }

    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition - GetMousePos();
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
    }

    private void OnMouseUp()
    {
        playerNotFound = !TryAttachToPlayer();
    }

    private bool TryAttachToPlayer()
    {
        bool temp = false;

        if (temp)
            return true;
        else
            return false;
    }
}
