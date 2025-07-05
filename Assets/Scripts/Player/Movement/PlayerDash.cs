using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    #region Links

    private Rigidbody2D rb;

    private PlayerGravity gravity;
    private Player player;

    private IEnumerator dashCoroutine;

    #endregion

    #region Public variables
    #endregion

    #region Private variables

    [SerializeField] private float dashTime;
    [SerializeField] private float dashSpeed;

    #endregion

    void Awake()
    {
        gravity = GetComponent<PlayerGravity>();
        player = GetComponent<Player>();

        rb = GetComponent<Rigidbody2D>();
    }

    public void HandleDash(in InputContext inputContext, ref PlayerContext playerContext)
    {
        if (!inputContext.dashPressed || inputContext.dashInput == Vector2.zero) { return; }

        if (playerContext.isDashing || !playerContext.canDash ) return;

        dashCoroutine = DashCoroutine(inputContext.dashInput, playerContext);
        StartCoroutine(dashCoroutine);
    }

    IEnumerator DashCoroutine(Vector3 dashDir, PlayerContext context)
    {
        Vector2 previousOrientation = context.Orientation;

        context.Orientation = dashDir;
        context.isDashing = true;
        context.canDash = false;

        rb.linearVelocity = context.Orientation * dashSpeed;

        yield return new WaitForSeconds(dashTime);

        context.isDashing = false;
        context.Orientation = previousOrientation;

        //rb.linearVelocity = Vector2.zero;


        //gravity?.Transform(previousOrientation);
    }

    public void StopDash(ref PlayerContext context)
    {
        context.canDash = true;
        context.isDashing = false;

        if (dashCoroutine != null)
        {
            StopCoroutine(dashCoroutine);
        }
    }

    void OnDrawGizmos()
    {
/*        if (Application.isPlaying)
        {
            Vector2 playerPosition = transform.position;
            Vector2 dashPosition = GetComponent<PlayerInput>().mousePosition - playerPosition;

            Gizmos.color = Color.white;

            Vector3 startPoint = playerPosition;
            Vector3 endPoint = startPoint + (Vector3)dashPosition;

            Gizmos.DrawLine(startPoint, endPoint);
        }*/
    }

}
