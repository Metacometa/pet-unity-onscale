using UnityEngine;

public class Player : MonoBehaviour
{
    #region Links

    private PlayerMovement movement;
    private PlayerJump jump;
    private PlayerDash dash;

    private PlayerInput input;
    private PlayerGravity gravity;
    private PlayerCollisionHandler collisionHandler;

    private Rigidbody2D rb;

    #endregion

    #region Public Variables

    public InputContext inputContext;
    public PlayerContext context;

    #endregion

    #region Private Variables


    #endregion

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        jump = GetComponent<PlayerJump>();
        dash = GetComponent<PlayerDash>();

        input = GetComponent<PlayerInput>();

        gravity = GetComponent<PlayerGravity>();
        collisionHandler = GetComponent<PlayerCollisionHandler>();


        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        input?.HandleInput(ref inputContext);
    }

    void FixedUpdate()
    {
        movement?.HandleMovement(inputContext, context);

        jump?.HandleJump(inputContext);
        dash?.HandleDash(inputContext);

        UpdateAirState();
        gravity?.HandleGravity();
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
        else if (jump.GroundCheck())
        {
            context.airState = AirState.Grounded;
        }
        else
        {
            context.airState = AirState.Null;
        }
    }
}
