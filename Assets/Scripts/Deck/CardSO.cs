using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/New Card")]
public class CardSO : ScriptableObject
{
    [SerializeField] int id;
    [SerializeField] int value;
    [SerializeField] bool isAce;
    [SerializeField] GameObject cardPrefab;

    public int GetId()
    {
        return id;
    }
    public int GetValue()
    {
        return value;
    }

    public GameObject GetCardPrefab()
    {
        return cardPrefab;
    }

    public Transform GetCardTransform()
    {
        return cardPrefab.transform;
    }
}
