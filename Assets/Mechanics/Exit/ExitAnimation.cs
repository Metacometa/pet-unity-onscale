using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitAnimation : MonoBehaviour
{
    private enum AnimationState { closed, closing, opened, opening }; 
    private Animator anim;
    private AnimationState state = AnimationState.closed;
    private Exit exit;
    [SerializeField] private string targetTag;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        exit = GetComponent<Exit>();
    }

    void Update()
    {
        anim.SetInteger("state", (int)state);
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag(targetTag) && exit.isPlayerContained) 
        {
            state = AnimationState.opening;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag(targetTag)) 
        {
            state = AnimationState.closing;
        }
    }

    public void DoorOpen()
    {
        state = AnimationState.opened;
    }

    public void DoorClose()
    {
        state = AnimationState.closed;
    }
}
