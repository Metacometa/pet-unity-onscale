using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    #region Links

    private Rigidbody2D rb;

    private PlayerJump jump;
    private Player player;

    private IEnumerator dashCoroutine;

    private PlayerContext context;

    #endregion

    #region Public variables
    #endregion

    #region Private variables

    [SerializeField] private float dashTime;
    [SerializeField] private float dashSpeed;

    [SerializeField] private float postDashSpeed;

    Vector2 previousOrientation = Vector2.zero;

    #endregion

    #region Callbacks

    void Awake()
    {
        jump = GetComponent<PlayerJump>();
        player = GetComponent<Player>();

        rb = GetComponent<Rigidbody2D>();

        context = player.context;

        EventManager.OnGravityColliding += StopDash;
        EventManager.OnGrounding += UpdateDash;
    }

    void Update()
    {
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

    void OnDestroy()
    {
        EventManager.OnGravityColliding -= StopDash;
        EventManager.OnGrounding -= UpdateDash;
    }

    #endregion

    #region Functions

    public void HandleDash(in InputContext inputContext)
    {
        if (!inputContext.dashPressed || inputContext.dashInput == Vector2.zero) { return; }

        if (context.isDashing || !context.canDash ) return;

        dashCoroutine = DashCoroutine(inputContext.dashInput);
        StartCoroutine(dashCoroutine);
    }

    IEnumerator DashCoroutine(Vector3 dashDir)
    {
        context.isDashing = true;
        context.canDash = false;

        rb.linearVelocity = dashDir * dashSpeed;

        yield return new WaitForSeconds(dashTime);

        rb.linearVelocity = dashDir * postDashSpeed;

        StopDash();
    }

    public void StopDash()
    {
        if (!context.isDashing) return;
        
        context.isDashing = false;

        if (dashCoroutine != null)
        {
            StopCoroutine(dashCoroutine);
        }
    }

    public void UpdateDash()
    {
        if (!context.isDashing)
        {
            context.canDash = true;

        }
    }
    
    #endregion
}
