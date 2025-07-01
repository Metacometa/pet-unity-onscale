using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerJumpNew : MonoBehaviour
{
    #region Links

    protected Rigidbody2D rb;

    private PlayerOrientation orientation;
    private PlayerDash dash;

    #endregion

    #region Private variables

    [SerializeField] private float jumpForce = 5f;

    #endregion

    #region Public variables
    public VerticalMovementState State => GetVerticalMovementState();

    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        orientation = GetComponent<PlayerOrientation>();
        dash = GetComponent<PlayerDash>();
    }

    void Update()
    {
        float angle = Mathf.Repeat(Vector2.SignedAngle(orientation.Dir, Vector2.right), 360f);
        Debug.Log($"Angle: { angle }");
        //Debug.Log($"VerticalMovingState: {GetVerticalMovementState()}");
    }

    public void Jump()
    {
        Vector2 jumpDir = -orientation.Dir;
        rb.AddForce(jumpDir * jumpForce, ForceMode2D.Impulse);
    }

    VerticalMovementState GetVerticalMovementState()
    {
        if (dash.isDashing) 
            return VerticalMovementState.Dashing;


        float verticalSpeed = Vector2.Dot(rb.linearVelocity.normalized, orientation.Dir);


        //Debug.Log($"VerticalSpeed: {verticalSpeed}");

        if (verticalSpeed > 0.1f)
        {
            return VerticalMovementState.Falling;
        }
        else if (verticalSpeed < -0.1f)
        {
            return VerticalMovementState.Jumping;
        }
        else if (GroundCheck())
        {
            return VerticalMovementState.Grounded;
        }

        return VerticalMovementState.Null;
    }

    #region GroundCheck
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    private bool GroundCheck()
    {
        return Physics2D.BoxCast(transform.position, boxSize, 0, orientation.Dir, castDistance, groundLayer);
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            if (GroundCheck())
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.red;
            }

            Gizmos.DrawWireCube((Vector2)transform.position + orientation.Dir * castDistance, boxSize);
        }
    }

    #endregion
}
