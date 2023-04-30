using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<Bot> currentPlayers = new List<Bot>();

    Bot[] players;
    Dealer dealer;

    private eGameState currentGameState;

    public static GameManager Instance;

    bool anyBotWaiting = true;

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
        CheckPlayersState();

        foreach (Bot bot in currentPlayers)
        {
            if (bot.GetCurrentState() == eState.BlackJack)
            {
                UpdateGameState(eGameState.PlayersWins);
                return;
            }
        }

        if (currentGameState == eGameState.DealerTurn)
        {
            if (dealer.GetCurrentState() == eState.Busted)
            {
                UpdateGameState(eGameState.PlayersWins);
                return;
            }
            else if (dealer.GetCurrentState() == eState.BlackJack)
            {
                UpdateGameState(eGameState.DealerWins);
                return;
            }
            else if (GetMaxNumber(currentPlayers) <= dealer.GetPoints())
            {
                UpdateGameState(eGameState.DealerWins);
                return;
            }

        }
    }

    private void InizializePlayers()
    {
        foreach (Bot player in players)
        {
            currentPlayers.Add(player);
        }
    }

    private void CheckPlayersState()
    {
        if (currentGameState == eGameState.PlayersTurn)
        {
            foreach (Bot bot in currentPlayers)
            {
                if (bot.GetCurrentState() == eState.Hit)
                {
                    anyBotWaiting = true;
                    break;
                }
                else
                {
                    anyBotWaiting = false;
                }
            }
            if (!anyBotWaiting)
            {
                Debug.Log("Nessun bot sta aspettando");
                UpdateGameState(eGameState.DealerTurn);
            }
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
            case eGameState.PlayersWins:
                Debug.Log("Players Wins!");
                break;
            case eGameState.DealerWins:
                Debug.Log("Dealer Wins!");
                break;
            default:
                Debug.Log("No State!");
                break;
        }
    }
    public int GetMaxNumber(List<Bot> players)
    {
        int max = 0;
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].GetPoints() > max)
            {
                max = players[i].GetPoints();
            }
        }
        return max;
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
    DealerWins,
    PlayersWins
}
