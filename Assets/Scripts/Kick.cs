using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : MonoBehaviour
{
    public float forwardForce;
    public float upwardForce;
    public enum Players { player1, player2 }
    public Players player;
    KeyCode kickKey;

    public bool isKicking;
    Collider col;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
        col.enabled = false;

        if (player == Players.player1)
        {
            kickKey = KeyCode.Comma;
        }
        else
        {
            kickKey = KeyCode.T;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(kickKey))
        {
            col.enabled = true;
            isKicking = true;
            Invoke("DeactivateKick", 0.25f);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            ActivateKick(other.GetComponent<Rigidbody>());
        }
    }
    
    void ActivateKick(Rigidbody target)
    {
        
        Vector3 direction = (target.transform.position - transform.position) * forwardForce + Vector3.up * upwardForce;
        target.AddForce(direction, ForceMode.Impulse);
        
    }
    void DeactivateKick()
    {
        col.enabled = false;
        isKicking = false;
    }
}
