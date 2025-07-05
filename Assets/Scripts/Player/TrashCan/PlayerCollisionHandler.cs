using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerCollisionHandler : MonoBehaviour
{
    #region Links

    private PlayerDash dash;

    #endregion

    #region Private Variables

    [SerializeField] private float gravitySphereRadius;
    [SerializeField] private LayerMask gravitySphereMask;

    #endregion

    void Awake()
    {
        dash = GetComponent<PlayerDash>();
    }

    public void HandleCollision(ref PlayerContext playerContext)
    {
        OrientToClosestGravityPlatform(ref playerContext);
    }

    void OrientToClosestGravityPlatform(ref PlayerContext playerContext)
    {
        Collider2D[] platforms = Physics2D.OverlapCircleAll(transform.position, gravitySphereRadius, gravitySphereMask);

        Vector2 closestPoint = Vector2.zero;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D platform in platforms)
        {
            Vector2 contactPoint = platform.ClosestPoint(transform.position);
            float distance = Vector2.Distance(transform.position, contactPoint);

            if (distance < closestDistance)
            {
                closestPoint = contactPoint;
                closestDistance = distance;
            }
        }

        if (closestPoint != Vector2.zero)
        {
            //dash?.StopDash(ref playerContext);
            Vector2 dir = closestPoint - (Vector2)transform.position;
            //context.Orientation = dir;
        }
    }
}