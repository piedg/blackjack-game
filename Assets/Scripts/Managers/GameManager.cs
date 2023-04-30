using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<Bot> currentPlayers = new List<Bot>();
    [SerializeField] List<Bot> bustedPlayers = new List<Bot>();

    int playersNumber;

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

        UpdateGameState(eGameState.PlayersTurn);
    }

    private void Update()
    {
        CheckTurn();

        // Win Conditions

        // TODO extract in functions

        if (bustedPlayers.Count == playersNumber)
        {
            Debug.Log("Entro qui 0, All players busted!");
            UpdateGameState(eGameState.DealerWins);
            return;
        }

        foreach (Bot bot in currentPlayers)
        {
            if (bot.GetCurrentState() == ePlayerState.BlackJack)
            {
                UpdateGameState(eGameState.PlayersWins);
                return;
            }
            else if (bot.GetCurrentState() == ePlayerState.Busted)
            {
                bustedPlayers.Add(bot);
                currentPlayers.Remove(bot);
                return;
            }
        }

        if (currentGameState == eGameState.DealerTurn)
        {
            Debug.Log("Current Dealer state: " + dealer.GetCurrentState());

            if (dealer.GetCurrentState() == ePlayerState.Busted)
            {
                Debug.Log("Entro qui 1");
                UpdateGameState(eGameState.PlayersWins);
                return;
            }
            else if (dealer.GetCurrentState() == ePlayerState.BlackJack)
            {
                Debug.Log("Entro qui 2");

                UpdateGameState(eGameState.DealerWins);
                return;
            }
            else if (dealer.GetPoints() >= GetMaxNumber(currentPlayers) && dealer.GetCurrentState() != ePlayerState.Busted)
            {
                Debug.Log("Entro qui 3");
                UpdateGameState(eGameState.DealerWins);
                return;
            }
        }
    }

    private void InizializePlayers()
    {
        playersNumber = players.Length;

        foreach (Bot player in players)
        {
            currentPlayers.Add(player);
        }
    }

    private void CheckTurn()
    {
        if (currentGameState == eGameState.PlayersTurn)
        {
            foreach (Bot bot in currentPlayers)
            {
                if (bot.GetCurrentState() == ePlayerState.Hit)
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
                UIManager.Instance.UpdateCurrentTurnText("Players Turn");
                break;
            case eGameState.DealerTurn:
                UIManager.Instance.UpdateCurrentTurnText("Dealer Turn");
                break;
            case eGameState.PlayersWins:
                UIManager.Instance.ShowWinnerText("Players Win!");
                break;
            case eGameState.DealerWins:
                UIManager.Instance.ShowWinnerText("Dealer Wins!");
                break;
            default:
                break;
        }
    }

    public void NextRound()
    {
        UpdateGameState(eGameState.PlayersTurn);

        // put current cards both dealer and players at the bottom of the deck

        // clear current cards both dealer and players

        foreach (Card card in dealer.GetCurrentCards())
        {
            Deck.Instance.AddCardToDeck(card);
        }
        dealer.ClearCurrentCards();

        foreach (Bot bot in currentPlayers)
        {
            foreach (Card card in bot.GetCurrentCards())
            {
               // Deck.Instance.AddCardToDeck(card);
            }
            bot.ClearCurrentCards();
        }

        // clear busted players
        bustedPlayers.Clear();
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
