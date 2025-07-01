using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerOrientation orientation;

    public bool isDashing = false;

    void Awake()
    {
        orientation = GetComponent<PlayerOrientation>();

        rb = GetComponent<Rigidbody2D>();
    }

    public void Dash(in Vector2 targetPosition)
    {
        Vector2 playerPosition = transform.position;
        Vector2 dashPosition = targetPosition - playerPosition;

        orientation.Dir = dashPosition;

        rb.linearVelocity = Vector3.zero;
    }
}
