using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    #region Links

    private Rigidbody2D rb;

    private PlayerOrientation orientation;
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
        orientation = GetComponent<PlayerOrientation>();
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

    public void Dash(in Vector2 targetPosition)
    {
        if (isDashing || !canDash) return;

        Vector2 playerPosition = transform.position;
        Vector2 dashPosition = playerPosition - targetPosition;

        dashCoroutine = DashCoroutine(dashPosition);
        StartCoroutine(dashCoroutine);
    }

    IEnumerator DashCoroutine(Vector3 dashPosition)
    {
        orientation.Dir = dashPosition;
        gravity?.Transform();

        //rb.linearVelocity = orientation.Dir * dashSpeed;

        isDashing = true;
        canDash = false;

        yield return new WaitForSeconds(dashTime);

        isDashing = false;
    }

    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Vector2 playerPosition = transform.position;
            Vector2 dashPosition = playerPosition - GetComponent<PlayerInput>().mousePosition;

            Gizmos.color = Color.white;

            Vector3 dir = rb.linearVelocity.normalized;

            Vector3 startPoint = playerPosition;
            Vector3 endPoint = startPoint + (Vector3)dashPosition;

            Gizmos.DrawLine(startPoint, endPoint);
        }
    }

}
