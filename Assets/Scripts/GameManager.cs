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

    private void Update()
    {
        CheckPlayersState();

        if(dealerTurn)
        {
            foreach (Bot player in players)
            {
                if(!player.IsBusted && !dealer.IsWaitingCard)
                {
                    if(player.GetPoints() > dealer.GetPoints())
                    {
                        Debug.Log("Player " + player.name + " Win!");
                    }
                    else
                    {
                        Debug.Log("Dealer win!");
                    }
                }
            }
        }
    }

    private void InizializePlayers()
    {
        foreach (Bot player in players)
        {
            currentPlayers.Add(player);
            player.IsWaitingCard = true;
        }
    }
    bool anyBotWaiting = false;

    private void CheckPlayersState()
    {
        foreach (Bot bot in currentPlayers)
        {
            if (bot.IsWaitingCard)
            {
                anyBotWaiting = true;
                break;
            }
            else
            {
                anyBotWaiting = false;
            }
        }

        if (anyBotWaiting)
        {
            // almeno un bot sta aspettando
            Debug.Log("Almeno un Bot sta aspettando");
        }
        else
        {
            // nessun bot sta aspettando la carta
            Debug.Log("Nessun bot sta aspettando");

            dealerTurn = true;
            dealer.IsWaitingCard = true;
        }
    }
}
