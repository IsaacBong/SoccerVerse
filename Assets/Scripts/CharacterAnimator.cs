using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    Kick kick;
    Sprint sprint;
    PlayerMovement move;

    Animator anim;

    float kickCooldown = 1f;
    float lastKick;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        kick = GetComponentInChildren<Kick>();
        sprint = GetComponent<Sprint>();
        move = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isSprinting", sprint.isSprinting);
        if(move.inputDirection != Vector3.zero)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        if (kick.isKicking && Time.time - lastKick > kickCooldown)
        {
            anim.SetTrigger("kick");
            lastKick = Time.time;
        }
    }
}
