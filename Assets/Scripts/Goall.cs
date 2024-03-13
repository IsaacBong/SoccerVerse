using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Goall : MonoBehaviour
{
    public UnityEvent score;
    public Volume volume;

    public void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            score.Invoke();
        }
    }
}
