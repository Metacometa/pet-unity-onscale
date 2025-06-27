using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    protected Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(in Vector2 dir)
    {
        rb.linearVelocity = new Vector2(dir.x * moveSpeed, rb.linearVelocity.y);
    }
}
