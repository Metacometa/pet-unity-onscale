using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    #region Link

    private Rigidbody2D rb;

    #endregion

    #region Variables

    [SerializeField] private float maxFallSpeed;

    private float gravityForce = -9.81f;

    [Space]
    [Header("Gravity")]
    [SerializeField] private float fallGravity;

    [SerializeField] private float gravity = 1;

    [Space]
    [Header("Orientation")]
    [SerializeField]/*[HideInInspector]*/ private Vector2 orientation;
    public Vector2 Orientation
    {
        get => orientation.normalized;
        set => orientation = value.normalized;
    }

    #endregion

    void Awake()
    {
        orientation = Vector2.down.normalized;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Debug.Log($"Magnitude: {rb.linearVelocity.magnitude}, Vector2: { rb.linearVelocity }");
    }


    public void Transform(in Vector2 newOrientation)
    { 
        Orientation = newOrientation;
        Physics2D.gravity = -Orientation * gravityForce;
    }

    public void ClampFallSpeed()
    {
        Vector2 horizontalDir = Vector2.Perpendicular(Orientation).normalized;

        float horizontalSpeed = Vector2.Dot(rb.linearVelocity, horizontalDir);

        float verticalSpeed = Vector2.Dot(rb.linearVelocity, Orientation);

        float limitedVerticalSpeed = Mathf.Clamp(verticalSpeed, -maxFallSpeed, maxFallSpeed);

        rb.linearVelocity = horizontalDir * horizontalSpeed + Orientation * limitedVerticalSpeed;
    }

    #region Gravity Setters

    public void SetFallGravity()
    {
        rb.gravityScale = Mathf.Lerp(rb.gravityScale, fallGravity, Time.fixedDeltaTime * 2);
        //rb.gravityScale = fallGravity;
    }

    public void SetDefaultGravity()
    {
        rb.gravityScale = gravity;
    }

    public void NullifyGravity()
    {
        rb.gravityScale = 0f;
    }

    #endregion
}
