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

        InizializePlayers();
    }

    private void InizializePlayers()
    {
        foreach (Bot player in players)
        {
            currentPlayers.Add(player);
            player.IsWaitingCard = true;
        }
    }
}
