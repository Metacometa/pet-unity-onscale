using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    #region Links

    private Player player;
    private PlayerDash dash;

    private PlayerContext playerContext;

    private Rigidbody2D rb;

    #endregion

    #region Private Variables

    [SerializeField] private float collisionRadius;
    [SerializeField] private LayerMask collisionLayerMask;

    #endregion

    #region Callbacks

    void Awake()
    {
        player = GetComponent<Player>();
        dash = GetComponent<PlayerDash>();

        playerContext = player.context;

        rb = GetComponent<Rigidbody2D>();

        EventManager.OnGrounding += ResetVelocity;
    }

    void OnDestroy()
    {
        EventManager.OnGrounding -= ResetVelocity;
    }

    void Update()
    {
        CollideWithPlatforms();
    }

    void CollideWithPlatforms()
    {
        Collider2D[] platforms = Physics2D.OverlapCircleAll(transform.position, collisionRadius, collisionLayerMask);

        Collider2D closestPlatform = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D platform in platforms)
        {
            Vector2 point = platform.ClosestPoint(transform.position);
            float distance = Vector2.Distance(point, transform.position); 

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlatform = platform;
            }
        }

        if (closestPlatform)
        {

            CollideWithPlatform(closestPlatform);
        }
    }

    void CollideWithPlatform(Collider2D collision)
    {
        Platform platform = collision.transform.GetComponent<Platform>();
        if (platform == null) return;

        //Debug.Log("CollideWithPlatform");

        EventManager.TriggerGrounding();

        switch (platform.type)
        {
            case PlatformType.Gravity:
                Vector2 closestPoint = collision.ClosestPoint(transform.position);
                Vector2 platformNormal = (Vector2)transform.position - closestPoint;

                //Vector2 obstacleNormal = collision.contacts[0].normal;
                playerContext.Orientation = -platformNormal.normalized;
                break;
            default:
                break;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
/*        Platform platform = collision.transform.GetComponent<Platform>();

        if (platform != null)
        {
            EventManager.TriggerGrounding();

            switch (platform.type)
            {
                case PlatformType.Gravity:
                    Vector2 obstacleNormal = collision.contacts[0].normal;
                    playerContext.Orientation = -obstacleNormal;
                    break;
                default:
                    break;
            }
        }*/
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, collisionRadius);
    }

    #endregion

    void ResetVelocity()
    {
        rb.linearVelocity = Vector2.zero;
    }
}