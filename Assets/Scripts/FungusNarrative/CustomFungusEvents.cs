using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class CustomFungusEvents : MonoBehaviour
{
    public void TriggerNarrativeBlock(Flowchart flowchart, string blockName)
    {
        // Your custom logic here
        flowchart.ExecuteBlock(blockName);
    }
}