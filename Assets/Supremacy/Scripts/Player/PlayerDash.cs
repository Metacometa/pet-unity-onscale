using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    #region Links

    private Rigidbody2D rb;

    private PlayerGravity gravity;

    private IEnumerator dashCoroutine;

    #endregion

    #region Public variables

    public bool isDashing = false;
    public bool canDash = false;

    #endregion

    #region Private variables

    [SerializeField] private float dashTime;
    [SerializeField] private float dashSpeed;

    #endregion

    void Awake()
    {
        gravity = GetComponent<PlayerGravity>();

        rb = GetComponent<Rigidbody2D>();
    }

    public void StopDash()
    {
        canDash = true;
        isDashing = false;

        if (dashCoroutine != null)
        {
            StopCoroutine(dashCoroutine);
        }
    }

    public void Dash(in Vector2 dashDir)
    {
        if (isDashing || !canDash || dashDir == Vector2.zero) return;

        dashCoroutine = DashCoroutine(dashDir);
        StartCoroutine(dashCoroutine);
    }

    IEnumerator DashCoroutine(Vector3 dashDir)
    {
        Vector2 previousOrientation = gravity.Orientation;

        gravity?.Transform(dashDir);
        gravity.NullifyGravity();

        rb.linearVelocity = gravity.Orientation * dashSpeed;

        isDashing = true;
        canDash = false;

        yield return new WaitForSeconds(dashTime);

        isDashing = false;

        rb.linearVelocity = Vector2.zero;
        gravity?.Transform(previousOrientation);
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
