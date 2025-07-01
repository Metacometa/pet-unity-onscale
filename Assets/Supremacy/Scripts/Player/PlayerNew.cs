using UnityEngine;

public class PlayerNew : MonoBehaviour
{
    private PlayerMovementNew movement;
    private PlayerJumpNew jump;
    private PlayerDash dash;

    private PlayerInput input;
    private PlayerOrientation orientation;
    private PlayerGravity gravity;

    private Rigidbody2D rb;

    void Awake()
    {
        movement = GetComponent<PlayerMovementNew>();
        jump = GetComponent<PlayerJumpNew>();
        dash = GetComponent<PlayerDash>();

        input = GetComponent<PlayerInput>();

        orientation = GetComponent<PlayerOrientation>();
        gravity = GetComponent<PlayerGravity>();

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {   
    }

    void FixedUpdate()
    {
        movement.Move(input.moveInput);

        if (input.jumpInput && jump.State == VerticalMovementState.Grounded)
        {
            jump.Jump();
        }

        if (input.dashInput)
        {
            //dash.Dash(input.mousePosition);
        }

        if (jump.State == VerticalMovementState.Falling)
        {
            gravity.ClampFallSpeed();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Obstacle"))
        {
            Vector2 obstacleNormal = collision.contacts[0].normal;
            Vector2 orientationDir = -obstacleNormal;

            orientation.RotateTo(orientationDir);
            gravity.Transform();
            //rb.linearVelocity = Vector2.zero;
        }  
    }
}
