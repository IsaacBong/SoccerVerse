using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

//using UnityEngine.Rendering.PostProcessing;

public class Goall : MonoBehaviour
{
    public UnityEvent score;
    private GameObject cam;
    private float lastValue;
    private LensDistortion Distortion;
    public Volume volume;

    private void Start()
    {
       volume.profile.TryGet<LensDistortion>(out Distortion);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            score.Invoke();
            
            Distortion.intensity.value = intensity;

        }
    }
}
