using UnityEngine;

public class PlayerNew : MonoBehaviour
{
    #region Links

    private PlayerMovementNew movement;
    private PlayerJumpNew jump;
    private PlayerDash dash;

    private PlayerInput input;
    private PlayerOrientation orientation;
    private PlayerGravity gravity;

    private Rigidbody2D rb;

    #endregion

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
        if (input.dashInput && dash?.canDash == true)
        {
            dash?.Dash(input.mousePosition);
        }

        if (jump?.State == VerticalMovementState.Falling)
        {
            gravity?.ClampFallSpeed();
            gravity?.SetFallGravity();
        }
        else if (jump?.State == VerticalMovementState.Dashing)
        {

        }
        else
        {
            gravity?.SetDefaultGravity();

            movement.Move(input.moveInput);

            if (input.jumpInput && jump?.State == VerticalMovementState.Grounded)
            {
                jump?.Jump();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Obstacle"))
        {
            Vector2 obstacleNormal = collision.contacts[0].normal;
            Vector2 orientationDir = -obstacleNormal;

            orientation?.RotateTo(orientationDir);
            gravity?.Transform();
            dash?.StopDash();
            //rb.linearVelocity = Vector2.zero;
        }  
    }
}
