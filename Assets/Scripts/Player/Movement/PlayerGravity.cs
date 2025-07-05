using Unity.VisualScripting;
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    #region Link

    private Rigidbody2D rb;

    #endregion

    #region Private Variables

    [Space]
    [Header("Gravity")]
    [SerializeField] private float deafultGravityScale = 1;
    [SerializeField] private float gravityForce = -9.81f;

    [Space]
    [Header("Fall Gravity")]
    [SerializeField] private float maxFallSpeed;
    [Space]
    [SerializeField] private float fallGravityScale;
    [SerializeField] private float fallGravityLerpSpeed = 1f;

    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void HandleGravity(in PlayerContext context)
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
            default:
                rb.gravityScale = deafultGravityScale;
                break;
        }
    }

    public void GravityShift(in PlayerContext context)
    { 
        Physics2D.gravity = -context.Orientation * gravityForce;
    }

    public void ClampFallSpeed(in PlayerContext context)
    {
        Vector2 horizontalDir = Vector2.Perpendicular(context.Orientation).normalized;

        float horizontalSpeed = Vector2.Dot(rb.linearVelocity, horizontalDir);

        float verticalSpeed = Vector2.Dot(rb.linearVelocity, context.Orientation);

        float limitedVerticalSpeed = Mathf.Clamp(verticalSpeed, -maxFallSpeed, maxFallSpeed);

        rb.linearVelocity = horizontalDir * horizontalSpeed + context.Orientation * limitedVerticalSpeed;
    }

    private void SetFallGravity()
    {
        rb.gravityScale = Mathf.Lerp(rb.gravityScale, fallGravityScale, 
            Time.fixedDeltaTime * fallGravityLerpSpeed);
    }
}
