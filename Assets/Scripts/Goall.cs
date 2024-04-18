using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Goall : MonoBehaviour
{
    public float effectDuration = 1.0f;
    public float maxIntensity = -0.8f;
    public float maxScale = -0.8f; // Maximum scale of the lens distortion effect

    public Volume postProcessVolume;
    private LensDistortion lensDistortion;

    private bool isEffectActive = false;
    private float effectTimer = 0.0f;

    public UnityEvent score;

    public void Start()
    {
        postProcessVolume.profile.TryGet(out lensDistortion);
    }

    void Update()
    {
        if (isEffectActive)
        {
            // Update the lens distortion intensity based on the remaining effect duration
            float t = effectTimer / effectDuration;
            lensDistortion.intensity.value = Mathf.Lerp(0.0f, maxIntensity, t);

            float x = effectTimer / effectDuration;
            lensDistortion.intensity.value = Mathf.Lerp(0.0f, maxIntensity, t);
            lensDistortion.scale.value = Mathf.Lerp(0.0f, maxScale, t); // Set the scale of the lens distortion

            effectTimer += Time.deltaTime;

            // Deactivate the effect when the duration is over
            if (effectTimer >= effectDuration)
            {
                isEffectActive = false;
                lensDistortion.intensity.value = 0.0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            score.Invoke();

            isEffectActive = true;
            effectTimer = 0.0f;
        }
    }
}
