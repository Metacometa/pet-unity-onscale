using UnityEngine;

public class GravityPhysics : MonoBehaviour
{
    public static Vector2 GetNormal(in Collider2D collision, in Vector2 point)
    {
        Vector2 closestPoint = collision.ClosestPoint(point);

        return (point - closestPoint).normalized;
    }

    public static Vector2 ProjectOnOrientation(in Vector2 vector, in Vector2 orientation)
    {
        Vector2 horizontalDir = Vector2.Perpendicular(orientation.normalized).normalized;
        Vector2 verticalDir = orientation.normalized;

        float horizontalProjection = Vector2.Dot(vector, horizontalDir);
        float verticalProjection = Vector2.Dot(vector, verticalDir);

        return new Vector2(horizontalProjection, verticalProjection);
    }
}
