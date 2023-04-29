using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<Bot> currentPlayers = new List<Bot>();
    Bot[] players;

    Dealer dealer;

    private eGameState currentGameState;

    public static GameManager Instance;

    //public bool playersTurn;
    //public bool dealerTurn;

    bool anyBotWaiting = false;

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

        currentGameState = eGameState.PlayersTurn;
    }

    private void Update()
    {
        if (currentGameState == eGameState.DealerTurn)
        {
            if (dealer.GetCurrentState() == eState.Busted)
            {
                UpdateGameState(eGameState.PlayerWin);
                return;
            }
            else if (dealer.GetCurrentState() == eState.BlackJack)
            {
                UpdateGameState(eGameState.DealerWin);
                return;
            }

            foreach (Bot player in players)
            {
                if (!player.IsBusted && !dealer.IsWaitingCard)
                {
                    if (player.GetPoints() > dealer.GetPoints())
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
        CheckPlayersState();
    }

    private void InizializePlayers()
    {
        foreach (Bot player in players)
        {
            currentPlayers.Add(player);
            player.IsWaitingCard = true;
        }
    }

    private void CheckPlayersState()
    {
        foreach (Bot bot in currentPlayers)
        {
            if (bot.GetCurrentState() == eState.Hit)
            {
                UpdateGameState(eGameState.PlayersTurn);
                anyBotWaiting = true;
                break;
            }
            else if(bot.GetCurrentState() == eState.BlackJack)
            {
                UpdateGameState(eGameState.PlayerWin);
                return;
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
            UpdateGameState(eGameState.DealerTurn);
            //dealerTurn = true;
            dealer.IsWaitingCard = true;
        }
    }

    private void UpdateGameState(eGameState newState)
    {
        currentGameState = newState;

        switch (newState)
        {
            case eGameState.PlayersTurn:
                Debug.Log("Players Turn!");
                break;
            case eGameState.DealerTurn:
                Debug.Log("Dealer Turn!");
                break;
            case eGameState.PlayerWin:
                Debug.Log("Players Win!");
                break;
            case eGameState.DealerWin:
                Debug.Log("Dealer Win!");
                break;
            default:
                Debug.Log("No State!");
                break;
        }
    }

    public eGameState CurrentGameState()
    {
        return currentGameState;
    }
}

public enum eGameState
{
    PlayersTurn,
    DealerTurn,
    DealerWin,
    PlayerWin
}
