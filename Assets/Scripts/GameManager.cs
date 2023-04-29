using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<Bot> currentPlayers = new List<Bot>();
    Bot[] players;

    Dealer dealer;

    public static GameManager Instance;

    public bool playersTurn;
    public bool dealerTurn;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    private void Start()
    {
        players = FindObjectsOfType<Bot>();
        dealer = FindObjectOfType<Dealer>();

        foreach (Bot player in players)
        {
            currentPlayers.Add(player);
        }

        playersTurn = true;
    }

    private void Update()
    {
        if(dealerTurn)
        {
            if(dealer.GetPoints() == 21)
            {
                Debug.Log("Dealer Has Black Jack!");
            }
            else if(dealer.GetPoints() > 21)
            {
                Debug.Log("Dealer Busted!");
            }
        }
    }
}
