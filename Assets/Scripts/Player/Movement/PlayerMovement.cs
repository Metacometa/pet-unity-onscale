using UnityEngine;
using UnityEngine.Windows;

public class PlayerMovement : Movement
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void HandleMovement(in InputContext inputContext, in PlayerContext playerContext)
    {
        if (playerContext.isDashing) { return; }

        float evaluatedInput = OrientInput(inputContext.moveInput, playerContext);

        Vector2 horizontalDir = Vector2.Perpendicular(playerContext.Orientation).normalized;

        float verticalSpeed = Vector2.Dot(rb.linearVelocity, playerContext.Orientation);

        rb.linearVelocity = horizontalDir * evaluatedInput * moveSpeed + playerContext.Orientation * verticalSpeed;
    }

    private float OrientInput(in Vector2 input, in PlayerContext context)
    {
        float angle = Mathf.Repeat(Vector2.SignedAngle(context.Orientation, Vector2.right), 360f);

        if ((0f <= angle && angle <= 45f) ||
                (315f <= angle && angle <= 360f))
        {
            return input.y;
        }
        else if (45f <= angle && angle <= 135f)
        {
            return input.x;
        }
        else if (135 <= angle && angle <= 225f)
        {
            return -input.y;
        }
        else if (225f < angle && angle <= 315f)
        {
            return -input.x;
        }

        return input.x;
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

}
