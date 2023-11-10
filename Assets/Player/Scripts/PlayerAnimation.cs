using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Rigidbody2D rb; 
    private Animator anim;
    private PlayerMovement movement;
    private PlayerScale scaler;
    private Player player;

    private enum AnimationState { idle, runningGlitching, running, exiting };

    private AnimationState state = AnimationState.idle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        scaler = GetComponent<PlayerScale>();
        player = GetComponent<Player>();
    }

    void Update()
    {   
        float scale = rb.transform.localScale.x;
  
        if (movement.horizontalInput > 0)
        {
            if (scaler.minScale == scale || scale == scaler.maxScale)
            {
                state = AnimationState.running;
            }
            else 
            {
                state = AnimationState.runningGlitching;               
            }
        }
        else if (movement.horizontalInput < 0)
        {
            if (scaler.minScale == scale || scale == scaler.maxScale)
            {
                state = AnimationState.running;
            }
            else 
            {
                state = AnimationState.runningGlitching;               
            }
        }
        
        if (movement.horizontalInput == 0)
        {
            state = AnimationState.idle; 
        }

        if (player.isEndingAnimation == true)
        {
            state = AnimationState.exiting;
        }

        anim.SetInteger("state", (int)state);
    }
}
