using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameMaster : MonoBehaviour
{
    GameData saveData = new GameData();

    #region Singleton
    public static GameMaster instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    //hold reference to the current players
    [HideInInspector] public PlayerData currentPlayer1;
    [HideInInspector] public PlayerData currentPlayer2;

    //hold a temp list of scores to be sorted for highscores
    public List<PlayerData> tempPlayers = new List<PlayerData>(10);

    //debug switches
    public bool debugButtons;
    public bool loadOnStart = true;

    //edit current players data like score and name
    public void Start()
    {
        if (loadOnStart)
        {
            LoadGame();
        }
        else
        {
            saveData = new GameData();
            CreateTempList();
        }
    }

    //create a temp list of all players, filled in with data from saveData
    public void CreateTempList()
    {
        //generate a blank list
        tempPlayers = new List<PlayerData>();

        //get the players from saveData and put them in the list
        for (int i = 0; i < saveData.playerNames.Length; i++) 
        {
            //Create a player profile
            PlayerData newPlayer = new PlayerData();

            //input the information from the savedata to the new player
            newPlayer.playerName = saveData.playerNames[i];
            newPlayer.score = saveData.score[i];
            /*
            //calculate the kdr and input it
            if (existingPlayer.death == 0) existingPlayer.kdr = existingPlayer.kills;
            else newPlayer.kdr = (float)newPlayer.kills / (float)newPlayer.Death;
            Add new player list*/
            tempPlayers.Add(newPlayer);
        }
    }
    //add our current players to the list
    //sort the list from highest to lowest scores
    public List<PlayerData> SortTempList(List<PlayerData> unSortedPlayers, bool addCurrentPlayers = false)
    {
        if (addCurrentPlayers)
        {
            //check if list already contains player 1
            if (tempPlayers.Find(p => p.playerName == currentPlayer1.playerName) == null)
            {
                tempPlayers.Add(currentPlayer1);
            }
            else //if the player already exists, then replace its score with your current score
            {
                PlayerData existingPlayer = tempPlayers.Find(p => p.playerName == currentPlayer1.playerName);
                existingPlayer.score = currentPlayer1.score;
                /*
                //calculate the kdr and input it
                if (existingPlayer.death == 0) existingPlayer.kdr = existingPlayer.kills;
                else existingPlayer.kdr = (float)existingPlayer.kills / (float)existingPlayer.Death;*/
            }
            //check if list already contains player 2
            if (tempPlayers.Find(p => p.playerName == currentPlayer2.playerName) == null)
            {
                tempPlayers.Add(currentPlayer2);
            }
            else //if the player already exists, then replace its score with your current score
            {
                PlayerData existingPlayer = tempPlayers.Find(p => p.playerName == currentPlayer2.playerName);
                existingPlayer.score = currentPlayer2.score;
                /*
                //calculate the kdr and input it
                if (existingPlayer.death == 0) existingPlayer.kdr = existingPlayer.kills;
                else existingPlayer.kdr = (float)existingPlayer.kills / (float)existingPlayer.Death;*/
            }

        }
        List<PlayerData> sortedPlayers = unSortedPlayers.OrderByDescending(p => p.score).ToList();
        return sortedPlayers;
    }
    //convert the list to simple data arrays
    //save the arrays to saveData
    public void SendHighScoresToSaveData(List<PlayerData> players)
    {
        for (int i = 0; i < 10; i++)
        {
            saveData.playerNames[i] = players[i].playerName;
            saveData.score[i] = players[i].score;
        }
    }

    //save the game
    public void SaveGame()
    {
        SortTempList(tempPlayers, false);
        SendHighScoresToSaveData(tempPlayers);

        saveData.lastPlayerName[0] = currentPlayer1.playerName;
        saveData.lastPlayerName[1] = currentPlayer2.playerName;

        SaveSystem.instance.SaveGame(saveData);
    }
        public void LoadGame()
        {
            //attempt to get a saveData file from the computer
            saveData = SaveSystem.instance.LoadGame();
            if(saveData == null)//create a new file if noone were found
            {
                saveData = new GameData();
                Debug.Log("No data was found, a new file was created instead");
            }

            currentPlayer1.playerName = saveData.lastPlayerName[0];
            currentPlayer2.playerName = saveData.lastPlayerName[1];
            CreateTempList();

        }

    #region debugging
    private void Update()
    {
        if (!debugButtons) return;

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (tempPlayers != null)
            {
                tempPlayers = SortTempList(tempPlayers, false);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RandomFillData();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            ClearData();
        }

    }

    #region debugging functions
    
    void ClearData()
    {
        foreach (PlayerData player in tempPlayers)
        {
            player.playerName = "";
            player.score = 0;
            /*player.kills = 0;
            player.deaths = 0;
            player.kd = 0;*/
        }
    }
    void RandomFillData()
        {
            //create possible letters to randomise from
            string glyphs = "abcdefghijklmnopqrstuvwxyz";

            foreach (PlayerData player in tempPlayers)
            {
                //generate a random name for the temp player
                int charAmount = Random.Range(3, 10);
                player.playerName = "";
                for (int i = 0; i < charAmount; i++)
                {
                    player.playerName += glyphs[Random.Range(0, glyphs.Length)];
                }
                //generate random score score
                player.score = Random.Range(0, 20);
                //generate random Kills score
                /*player.kills = Random.Range(0, 20);
                //generate random deaths
                player.deaths = Random.Range(0, 20);
                //calculate kd
                if (player.deaths == 0) player.kd = player.kills;
                else player.kd = player.kills / player.deaths;*/
            }

        }
    #endregion
    #endregion
    

    #region OldCode
    /*
    private void Update()
    {
    if(Input.GetKeyDown(KeyCode.UpArrow))
    {
        saveData.AddScore(1);
        PrintScore();
    }
    if (Input.GetKeyDown(KeyCode.DownArrow))
    {
        saveData.AddScore(-1);
        PrintScore();
    }
    if (Input.GetKeyDown(KeyCode.S)) 
    {
        SaveSystem.instance.SaveGame(saveData);
        Debug.Log("Saved Game");
    }
    if (Input.GetKeyDown(KeyCode.L))
    {
        saveData = SaveSystem.instance.LoadGame();
        Debug.Log("Saved Game");
    }
    if (Input.GetKeyDown(KeyCode.X))
    {
        saveData.ResetData();
        PrintScore();
    }
}
public void PrintScore()
{
    Debug.Log("The current score is " + saveData.score);
}*/
    #endregion
}
