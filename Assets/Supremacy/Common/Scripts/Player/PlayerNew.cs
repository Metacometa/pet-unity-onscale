using UnityEngine;

public class PlayerNew : MonoBehaviour
{
    private PlayerMovementNew movement;
    private PlayerInput input;

    void Awake()
    {
        movement = GetComponent<PlayerMovementNew>();      
        input = GetComponent<PlayerInput>();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        movement.Move(input.moveInput);
    }
}
