using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Goall : MonoBehaviour
{
    public UnityEvent score;
    private GameObject cam;
    private float lastValue;
    private LensDistortion Distortion;
    public VolumeProfile profile;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            score.Invoke();
            profile.TryGet<LensDistortion>(out Distortion);
        }
    }
}
