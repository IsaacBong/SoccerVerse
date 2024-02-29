using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grower : MonoBehaviour, IInteraction
{
    public void Interact()
    {
        transform.localScale *= 2;
    }
}
