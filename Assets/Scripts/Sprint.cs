using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : MonoBehaviour
{
    public float speedMultiplier;
    public float sprintDuration;
    public float sprintCoolDown;

    public bool isSprinting;
    float lastSprint;

    PlayerMovement moveScript;

    public enum Players { player1, player2 }
    public Players player;
    KeyCode sprintKey;

    // Start is called before the first frame update
    void Start()
    {
        isSprinting = false;
        lastSprint = Time.time;
        moveScript = GetComponent<PlayerMovement>();

        if (player == Players.player1)
        {
            sprintKey = KeyCode.Period;
        }
        else
        {
            sprintKey = KeyCode.Y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(sprintKey) && (Time.time - lastSprint) > sprintCoolDown && !isSprinting)
        {
            StartSprint();
        }
    }
    void StartSprint()
    {
        isSprinting = true;
        moveScript.movementSpeed *= speedMultiplier;
        Invoke("EndSprint", sprintDuration);
    }
    void EndSprint()
    {
        isSprinting = false;
        moveScript.movementSpeed /= speedMultiplier;
        lastSprint = Time.time;
    }
}
