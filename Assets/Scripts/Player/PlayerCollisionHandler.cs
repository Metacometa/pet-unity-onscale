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
        CheckPlatforms();
    }

    void CheckPlatforms()
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
            PlatformInteraction(closestPlatform);
        }
    }

    void PlatformInteraction(Collider2D collision)
    {
        Platform platform = collision.transform.GetComponent<Platform>();
        if (platform == null) return;


        EventManager.TriggerGrounding();

        switch (platform.type)
        {
            case PlatformType.Gravity:    
                Vector2 platformNormal = GravityPhysics.GetNormal(collision, transform.position);
                playerContext.Orientation = -platformNormal.normalized;
                break;
            default:
                break;
        }
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