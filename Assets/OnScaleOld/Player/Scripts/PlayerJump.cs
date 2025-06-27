using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private LayerMask[] jumpableGround;
    [SerializeField] private float jumpForce = 5f;

    private Player player;

    public float verticalInput;

    private Collider2D coll;

    void Start()
    {
        player = GetComponent<Player>();
        coll = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (player.moveDirection.y > 0)
        {
            verticalInput = 1;
        }
        else
        {
            verticalInput = 0;
        }
    }
    public void Jump()
    {
        if ((verticalInput > 0) && IsGrounded())
        {
            //player.rb.velocity = new Vector2(player.rb.velocity.x, jumpForce);
            player.rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.down, Time.deltaTime, LayerMask.GetMask("Obstacle"));

        Draw(hit, coll.bounds.center, coll.bounds.size, 0, Vector2.down, Time.deltaTime);
    
        return hit.collider != null;
    }

public static void Draw(
        RaycastHit2D hitInfo,
        Vector2 origin,
        Vector2 size,
        float angle,
        Vector2 direction,
        float distance = Mathf.Infinity)
    {
        // Set up points to draw the cast.
        Vector2[] originalBox = CreateOriginalBox(origin, size, angle);

        Vector2 distanceVector = GetDistanceVector(distance, direction);
        Vector2[] shiftedBox = CreateShiftedBox(originalBox, distanceVector);

        // Draw the cast.
        Color castColor = hitInfo ? Color.red : Color.green;
        DrawBox(originalBox, castColor);
        DrawBox(shiftedBox, castColor);
        ConnectBoxes(originalBox, shiftedBox, Color.gray);

        if (hitInfo)
        {
            Debug.DrawLine(hitInfo.point, hitInfo.point + (hitInfo.normal.normalized * 0.2f), Color.yellow);
        }
    }
    private static Vector2[] CreateOriginalBox(Vector2 origin, Vector2 size, float angle)
    {
        float w = size.x * 0.5f;
        float h = size.y * 0.5f;
        Quaternion q = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));

        var box = new Vector2[4]
        {
            new Vector2(-w, h),
            new Vector2(w, h),
            new Vector2(w, -h),
            new Vector2(-w, -h),
        };

        for (int i = 0; i < 4; i++)
        {
            box[i] = (Vector2)(q * box[i]) + origin;
        }

        return box;
    }

    private static Vector2[] CreateShiftedBox(Vector2[] box, Vector2 distance)
    {
        var shiftedBox = new Vector2[4];
        for (int i = 0; i < 4; i++)
        {
            shiftedBox[i] = box[i] + distance;
        }

        return shiftedBox;
    }

    private static void DrawBox(Vector2[] box, Color color)
    {
        Debug.DrawLine(box[0], box[1], color);
        Debug.DrawLine(box[1], box[2], color);
        Debug.DrawLine(box[2], box[3], color);
        Debug.DrawLine(box[3], box[0], color);
    }

    private static void ConnectBoxes(Vector2[] firstBox, Vector2[] secondBox, Color color)
    {
        Debug.DrawLine(firstBox[0], secondBox[0], color);
        Debug.DrawLine(firstBox[1], secondBox[1], color);
        Debug.DrawLine(firstBox[2], secondBox[2], color);
        Debug.DrawLine(firstBox[3], secondBox[3], color);
    }

    private static Vector2 GetDistanceVector(float distance, Vector2 direction)
    {
        if (distance == Mathf.Infinity)
        {
            // Draw some large distance e.g. 5 scene widths long.
            float sceneWidth = Camera.main.orthographicSize * Camera.main.aspect * 2f;
            distance = sceneWidth * 5f;
        }

        return direction.normalized * distance;
    }
}
