using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Links

    private PlayerMovement movement;
    private PlayerJump jump;
    private PlayerDash dash;

    private PlayerInput input;
    private PlayerGravity gravity;

    private Rigidbody2D rb;

    #endregion

    #region Public variables

    public InputContext inputContext;
    public PlayerContext context;

    #endregion

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        jump = GetComponent<PlayerJump>();
        dash = GetComponent<PlayerDash>();

        input = GetComponent<PlayerInput>();

        gravity = GetComponent<PlayerGravity>();

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        input?.HandleInput(ref inputContext);
        UpdateAirState();
    }

    void FixedUpdate()
    {
        movement?.HandleMovement(inputContext, context);

        jump?.HandleJump(inputContext, context);
        dash?.HandleDash(inputContext, ref context);

        gravity?.HandleGravity(context);

/*        if (input.dashPressed && dash?.canDash == true)
        {
            dash?.Dash(input.dashInput);
        }

        if (AirState == AirState.Dashing)
        {
            return;
        }
        else if (AirState == AirState.Falling)
        {
            gravity?.ClampFallSpeed();
            gravity?.SetFallGravity();
        }
        else
        {
            gravity?.SetDefaultGravity();
        }

        movement.Move(input.moveInput);

        if (input.jumpPressed && AirState == AirState.Grounded)
        {
            jump?.Jump();
        }*/
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Obstacle"))
        {
            Vector2 obstacleNormal = collision.contacts[0].normal;

            context.Orientation = -obstacleNormal;
            //gravity?.Transform();

            dash?.StopDash(ref context);
            //rb.linearVelocity = Vector2.zero;
        }  
    }

    void UpdateAirState()
    {
        if (context.isDashing)
        {
            context.airState = AirState.Dashing;
            return;
        }

        float verticalSpeed = Vector2.Dot(rb.linearVelocity.normalized, context.Orientation);

        if (verticalSpeed > 0.1f)
        {
            context.airState = AirState.Falling;
        }
        else if (verticalSpeed < -0.1f)
        {
            context.airState = AirState.Jumping;
        }
        else if (jump.GroundCheck(context))
        {
            context.airState = AirState.Grounded;
        }
        else
        {
            context.airState = AirState.Null;
        }
    }
}
