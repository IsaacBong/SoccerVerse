using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class TriggerNarrative : MonoBehaviour
{
    public int goalsScored = 0;
    public Flowchart narrativeFlowchart;

    public bool playerScoredGoal = false; // Define the variable here

    void Update()
    {
        // Check for goal scoring condition here
        if (playerScoredGoal)
        {
            IncrementGoals();
            playerScoredGoal = false; // Reset the variable after handling the goal
        }
    }

    void IncrementGoals()
    {
        goalsScored++;
        if(goalsScored == 3)
        {
            // Trigger the narrative sequence
            narrativeFlowchart.ExecuteBlock("Outro");
        }
    }
}
