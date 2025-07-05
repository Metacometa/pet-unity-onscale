using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    #region Links

    protected Rigidbody2D rb;

    private PlayerGravity gravity;
    private PlayerDash dash;

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
    }

    public void HandleJump(in InputContext inputContext, in PlayerContext playerContext)
    {
        if (!inputContext.jumpPressed) return;

        if (playerContext.airState == AirState.Grounded)
        {
            Vector2 jumpDir = -playerContext.Orientation;
            rb.AddForce(jumpDir * jumpForce, ForceMode2D.Force);
        }
    }

    #region GroundCheck

    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    public bool GroundCheck(in PlayerContext context)
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
