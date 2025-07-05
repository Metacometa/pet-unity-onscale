using UnityEngine;

public class PlayerMovementNew : Movement
{
    private PlayerGravity gravity;

    protected override void Awake()
    {
        base.Awake();
        gravity = GetComponent<PlayerGravity>();
    }

    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.yellow;

            Vector3 dir = rb.linearVelocity.normalized;

            Vector3 startPoint = transform.position;
            Vector3 endPoint = startPoint + dir;

            Gizmos.DrawLine(startPoint, endPoint);
        }
    }

    public override void Move(in Vector2 input)
    {
        float evaluatedInput = input.x;
        float angle = Mathf.Repeat(Vector2.SignedAngle(gravity.Orientation, Vector2.right), 360f);

        if ((0f <= angle && angle <= 45f) ||
                (315f <= angle && angle <= 360f))
        {
            evaluatedInput = input.y;
        }
        else if (45f <= angle && angle <= 135f)
        {
            evaluatedInput = input.x;
        }
        else if (135 <= angle && angle <= 225f)
        {
            evaluatedInput = -input.y;
        }
        else if (225f < angle && angle <= 315f)
        {
            evaluatedInput = -input.x;
        }

        Vector2 horizontalDir = Vector2.Perpendicular(gravity.Orientation).normalized;

        float verticalSpeed = Vector2.Dot(rb.linearVelocity, gravity.Orientation);

        rb.linearVelocity = horizontalDir * evaluatedInput * moveSpeed + gravity.Orientation * verticalSpeed;
    }
}
