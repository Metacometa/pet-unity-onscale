using Unity.VisualScripting;
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    #region Link

    private Rigidbody2D rb;

    private Player player;
    private PlayerContext context;

    #endregion

    #region Private Variables

    [Space]
    [Header("Gravity")]
    [SerializeField] private float deafultGravityScale = 10;
    [SerializeField] private float gravityForce = -9.81f;


    [Space]
    [Header("Jump Gravity")]

    [SerializeField] private float jumpGravityScale = 1;

    [Space]
    [Header("Fall Gravity")]
    [SerializeField] private float maxFallSpeed;
    [Space]
    [SerializeField] private float fallGravityScale;
    [SerializeField] private float fallGravityLerpSpeed = 1f;

    #endregion

    #region Callbacks

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        player = GetComponent<Player>();
        context = player.context;
    }

    void OnDestroy()
    {
    }

    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;

            Vector3 dir = context.Orientation;

            Vector3 startPoint = transform.position;
            Vector3 endPoint = startPoint + dir * 2f;

            Gizmos.DrawLine(startPoint, endPoint);
        }
    }

    #endregion

    #region Functions

    public void HandleGravity()
    {
        GravityShift(context);

        switch (context.airState)
        {
            case AirState.Dashing:
                rb.gravityScale = 0f;
                break;
            case AirState.Falling:
                SetFallGravity();
                ClampFallSpeed(context);
                break;
            case AirState.Jumping:
                rb.gravityScale = jumpGravityScale;
                break;
            case AirState.Grounded:
                rb.gravityScale = deafultGravityScale;
                break;
            default:
                SetFallGravity();
                ClampFallSpeed(context);
                break;
        }
    }

    public void GravityShift(in PlayerContext context)
    { 
        Physics2D.gravity = -context.Orientation * gravityForce;
    }

    public void ClampFallSpeed(in PlayerContext context)
    {
        Vector2 projection = VectorMath.ProjectOnOrientation(rb.linearVelocity, context.Orientation);
        float limitedVerticalSpeed = Mathf.Clamp(projection.y, -maxFallSpeed, maxFallSpeed);

        rb.linearVelocity = Vector2.Perpendicular(context.Orientation).normalized * projection.x + 
            context.Orientation * limitedVerticalSpeed;
    }

    void SetFallGravity()
    {
        rb.gravityScale = Mathf.Lerp(rb.gravityScale, fallGravityScale, 
            Time.fixedDeltaTime * fallGravityLerpSpeed);
    }

    public void ResetVelocity()
    {
        rb.linearVelocity = Vector2.zero;
    }

    public void ResetOrientation()
    {
        context.Orientation = Vector2.zero;
    }

    #endregion
}
