using System.Net.Mime;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    #region Links

    protected Rigidbody2D rb;

    private PlayerGravity gravity;
    private PlayerDash dash;

    private Player player;
    private PlayerContext context;

    #endregion

    #region Private variables

    [SerializeField] private float jumpForce = 5f;

    #endregion

    #region Public variables

    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        gravity = GetComponent<PlayerGravity>();
        dash = GetComponent<PlayerDash>();

        player = GetComponent<Player>();
        context = player.context;
    }

    public void HandleJump(in InputContext inputContext)
    {
        if (!inputContext.jumpPressed) return;

        if (context.airState == AirState.Grounded)
        {
            Vector2 jumpDir = -context.Orientation;
            rb.AddForce(jumpDir * jumpForce, ForceMode2D.Force);
        }
    }

    #region GroundCheck

    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    public bool GroundCheck()
    {
        return Physics2D.BoxCast(transform.position, boxSize, 0, context.Orientation, castDistance, groundLayer);
    }

    private void OnDrawGizmos()
    {
/*        if (Application.isPlaying)
        {
            if (GroundCheck())
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.red;
            }

            Gizmos.DrawWireCube((Vector2)transform.position + gravity.Orientation * castDistance, boxSize);
        }*/
    }

    #endregion
}
