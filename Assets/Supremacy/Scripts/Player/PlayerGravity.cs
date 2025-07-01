using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    private PlayerOrientation orientation;

    [SerializeField] private float maxFallSpeed;

    private float gravityForce = -9.81f;

    private Rigidbody2D rb;

    void Awake()
    {
        orientation = GetComponent<PlayerOrientation>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Debug.Log($"Magnitude: {rb.linearVelocity.magnitude}, Vector2: { rb.linearVelocity }");
    }


    public void Transform()
    {
        Physics2D.gravity = -orientation.Dir * gravityForce;
    }

    public void ClampFallSpeed()
    {
        Vector2 horizontalDir = Vector2.Perpendicular(orientation.Dir).normalized;

        float horizontalSpeed = Vector2.Dot(rb.linearVelocity, horizontalDir);

        float verticalSpeed = Vector2.Dot(rb.linearVelocity, orientation.Dir);

        float limitedVerticalSpeed = Mathf.Clamp(verticalSpeed, -maxFallSpeed, maxFallSpeed);

        rb.linearVelocity = horizontalDir * horizontalSpeed + orientation.Dir * limitedVerticalSpeed;
    }
}
