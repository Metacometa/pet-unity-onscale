using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    #region Links

    private Player player;
    private PlayerDash dash;

    private PlayerContext playerContext;

    private Rigidbody2D rb;

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

    void OnCollisionStay2D(Collision2D collision)
    {
        Platform platform = collision.transform.GetComponent<Platform>();

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
        }
    }

    #endregion

    void ResetVelocity()
    {
        rb.linearVelocity = Vector2.zero;
    }
}