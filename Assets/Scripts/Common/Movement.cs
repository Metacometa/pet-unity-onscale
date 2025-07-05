using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 5f;
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Move(in Vector2 dir)
    {
/*        Vector2 orientatedDir = Vector2.Perpendicular(dir).normalized;
        rb.linearVelocity = new Vector2(orientatedDir.x * moveSpeed, rb.linearVelocity.y);*/
    }

    public virtual void Move(float input)
    {

    }
}
