using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviour
{
    //player scores
    public int player1, player2;

    //game state
    public bool gameOver = false;
    public bool isOnline = false;

    //timer
    public float startTime;
    float currentTime;

    //ui display
    public TMP_Text score, timer, result;
    public Color color1, color2, color3;
    public GameObject finishScreen;

    //ball
    public Rigidbody ball;

    //spawn point
    public Transform spawnPoint;

    //create a variable for the photon view component
    public PhotonView PhotonView;

    //variables for spawn points for players and ball
    public GameObject PrefabP1;
    public GameObject PrefabP2;
    public GameObject PrefabBall;
    public GameObject P1SpawnPoint;
    public GameObject P2SpawnPoint;
    public GameObject BallSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer());
        PhotonView = GetComponent<PhotonView>();
        //get the photon view variable

        if (isOnline)
        {
            if (PhotonNetwork.IsMasterClient == true) 
            {
                PhotonNetwork.Instantiate(PrefabP1.name, P1SpawnPoint.transform.position, Quaternion.LookRotation(Vector3.right));
                ball =  PhotonNetwork.Instantiate(PrefabBall.name, BallSpawnPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                
            }
            else
            {
                PhotonNetwork.Instantiate(PrefabP2.name, P2SpawnPoint.transform.position, Quaternion.LookRotation(Vector3.left));
            }
        }
        
        //check if player 1, then spawn player 1 object at location vis versa
        //check if master client and spawn ball
    }

    //score goal
    public void Goal(int playerNum)
    {
        if (gameOver == true) return;

        if(playerNum == 1)
        {
            player1++;
        }
        else
        {
            player2++;
        }
        score.text = player1 + " | " + player2;

        //only reset ball if master client
        //if (isOnline && !photonnetwork.isMasterClient) return;
        StartCoroutine(ResetBall());
        
    }
    //timer coroutine
    public IEnumerator Timer()
    {
        //set the time
        currentTime = startTime;

        //Our loop will be repeated every second. Cashing a waitforsecnds to use every loop is
        //more performant than creating a new one every time.
        WaitForSeconds pause = new WaitForSeconds(1);

        //create a loop that repeats every second until current Time is 0
        while(currentTime > 0)
        {
            currentTime -= 1;
            var minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);

            string message = minutes.ToString("00") + ":" + seconds.ToString("00");
            timer.text = message;
            yield return pause;
        }
        gameOver = true;
        GameOver();
        yield return null;
    }
    //ball respawn coroutine
    public IEnumerator ResetBall()
    {
        ball.constraints = RigidbodyConstraints.FreezeAll;
        ball.useGravity = false;
        ball.gameObject.SetActive(false);

        yield return new WaitForSeconds(1);

        ball.transform.position = spawnPoint.position;
        ball.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        ball.constraints = RigidbodyConstraints.None;
        ball.useGravity = true;

        yield return null;
    }

    //game over
    void GameOver()
    {
        if(player1 > player2)
        {
            finishScreen.GetComponent<Image>().color = color1;
            result.text = "Player 1 Wins" + "\n" + player1.ToString("00") + "|" + player2.ToString("00");
        }
        else if (player2 > player1)
        {
            finishScreen.GetComponent<Image>().color = color2;
            result.text = "Player 2 Wins" + "\n" + player1.ToString("00") + "|" + player2.ToString("00");
        }
        else
        {
            finishScreen.GetComponent<Image>().color = color3;
            result.text = "TIE" + "\n" + player1.ToString("00") + "|" + player2.ToString("00");
        }
        GameMaster.instance.SaveGame();
        finishScreen.SetActive(true);
    }
}
