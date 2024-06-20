using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class LevelLoader : MonoBehaviour
{
    public bool isOnline = false;

    public void LoadNextLevel()
    {
        if (isOnline && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }          
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")LoadNextLevel();
    }
}
