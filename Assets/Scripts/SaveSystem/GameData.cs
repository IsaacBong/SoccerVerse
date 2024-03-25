using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //you can only save simple data
    public string[] playerNames = new string[10];
    //store arrays of our score information MAKE IT DIFFERENT FOR YOU
    public int[] score = new int[10];

    //Game settings or information
    public float maxRoundTime = 120f;
    public int maxKills = 10;

    public string[] lastPlayerName = new string[2];
}
