using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    #region Links

    private Player player;
    private PlayerDash dash;
    private PlayerJump jump;

    private PlayerContext playerContext;

    private Rigidbody2D rb;

    #endregion

    #region Private Variables

    [Header("Collisions")]
    [SerializeField] private float collisionRadius;
    [SerializeField] private LayerMask collisionLayerMask;

/*    [Space]

    [Header("Materials")]
    [SerializeField] private PhysicsMaterial2D groundedMaterial;
    [SerializeField] private PhysicsMaterial2D inAirMaterial;*/

    #endregion

    #region Callbacks

    void Awake()
    {
        player = GetComponent<Player>();
        dash = GetComponent<PlayerDash>();
        jump = GetComponent<PlayerJump>();

        playerContext = player.context;

        rb = GetComponent<Rigidbody2D>();

        EventManager.OnGravityColliding += ResetVelocity;
    }

    void OnDestroy()
    {
        EventManager.OnGravityColliding -= ResetVelocity;
    }

    void Update()
    {
        CheckPlatforms();

        bool groundCheck = (bool)(jump?.GroundCheck());
        if (groundCheck)
        {
            EventManager.TriggerGrounding();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, collisionRadius);
    }

    #endregion

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

        switch (platform.type)
        {
            case PlatformType.Gravity:
                Vector2 platformNormal = VectorMath.GetNormal(collision, transform.position);
                playerContext.Orientation = -platformNormal.normalized;

                EventManager.TriggerGravityColliding();
                break;
            case PlatformType.Normal:

                break;
            default:
                break;
        }
    }

    void ResetVelocity()
    {
        rb.linearVelocity = Vector2.zero;
    }
}