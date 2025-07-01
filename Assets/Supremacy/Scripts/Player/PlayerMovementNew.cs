using UnityEngine;

public class PlayerMovementNew : Movement
{
    private PlayerOrientation orientation;

    protected override void Awake()
    {
        base.Awake();
        orientation = GetComponent<PlayerOrientation>();
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

            //Debug.DrawRay(transform.position, dir, Color.red);
        }

    }

    public override void Move(in Vector2 input)
    {
        float evaluatedInput = input.x;
        float angle = Mathf.Repeat(Vector2.SignedAngle(orientation.Dir, Vector2.right), 360f);

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
            evaluatedInput = input.x;
        }



        if (orientation.Dir.y > 0)
        {
            evaluatedInput = -input.x;
        }



        //float filtredInput = ;

        Vector2 horizontalDir = Vector2.Perpendicular(orientation.Dir).normalized;

        float verticalSpeed = Vector2.Dot(rb.linearVelocity, orientation.Dir);

        rb.linearVelocity = horizontalDir * evaluatedInput * moveSpeed + orientation.Dir * verticalSpeed;

/*        Vector2 moveDirection = new Vector2(input * xDir.x * moveSpeed,
            rb.linearVelocityY);


        rb.linearVelocity += xDir * moveSpeed * Time.fixedDeltaTime;
        rb.linearVelocity = moveDirection;*/
    }
}
