using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<Bot> currentPlayers = new List<Bot>();
    Bot[] players;

    public static GameManager Instance;

    int firstTurn;

    private void Awake()
    {
        if(Instance == null)
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

        foreach (Bot player in players)
        {
            currentPlayers.Add(player);
        }
    }

    private void Update()
    {
      
    }

    public void GetPlayerStatus()
    {
        foreach (Bot player in currentPlayers)
        {

        }
    }
}
