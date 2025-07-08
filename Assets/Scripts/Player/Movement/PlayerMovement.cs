using UnityEngine;

public class PlayerMovement : Movement
{
    #region Callbacks

    protected override void Awake()
    {
        base.Awake();
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

    #endregion

    #region Functions

    public void HandleMovement(in InputContext inputContext, in PlayerContext playerContext)
    {
        if (playerContext.isDashing) { return; }

        Vector2 horizontalDir = Vector2.Perpendicular(playerContext.Orientation).normalized;

        float verticalSpeed = Vector2.Dot(rb.linearVelocity, playerContext.Orientation);
        float horizontalSpeed = Vector2.Dot(inputContext.moveInput, horizontalDir);

        float moveSpeed = airMoveSpeed;
        if (playerContext.airState == AirState.Grounded)
        {
            moveSpeed = groundMoveSpeed;
        }

        rb.linearVelocity = horizontalDir * horizontalSpeed * moveSpeed + playerContext.Orientation * verticalSpeed;
    }

    #endregion
}
