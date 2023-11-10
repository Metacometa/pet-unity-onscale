using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerScale scale;
    private PlayerJump jump;
    private PlayerUnscaleableZone unscaleableZone;
    public Vector2 moveDirection;
    [SerializeField] private InputAction action;
    [SerializeField] private string exitTag;

    public Rigidbody2D rb;
    private Collider2D coll;
    private SpriteRenderer sprite;

    public bool isEndingAnimation = false;
    public bool isLevelEnded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();

        movement = GetComponent<PlayerMovement>();
        scale = GetComponent<PlayerScale>();
        jump = GetComponent<PlayerJump>();
        unscaleableZone = GetComponent<PlayerUnscaleableZone>();
    }
    void Update()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.Space))
        {
            moveDirection.y = 1;
        }
        else
        {
            moveDirection.y = 0;
        }
    }

    void FixedUpdate()
    {
        if (isEndingAnimation == false)
        {
            movement.Move();
            jump.Jump();

            if (unscaleableZone.isScaleable == true)
            {
                scale.ScaleByMoving();
            }

            Flip();
        }
        else
        {
            scale.ExitingScaling();
        }
    }

    public void EndLevel()
    {
        isLevelEnded = true;
    }

    public bool IsContained(Collider2D maybeSmallerCollider, Collider2D maybeBiggerCollider)
    {
        return maybeSmallerCollider.bounds.max.x <= maybeBiggerCollider.bounds.max.x &&
            maybeSmallerCollider.bounds.min.x >= maybeBiggerCollider.bounds.min.x &&
            maybeSmallerCollider.bounds.max.y <= maybeBiggerCollider.bounds.max.y &&
            maybeSmallerCollider.bounds.min.y >= maybeBiggerCollider.bounds.min.y;
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag(exitTag) && IsContained(coll, collider) && isEndingAnimation == false)
        {
            isEndingAnimation = true;
            rb.velocity = Vector2.zero;
            Vector2 exitingPosition = new(collider.transform.position.x, collider.transform.position.y - 1 + transform.localScale.y);
            rb.transform.position = exitingPosition;
        }
    }

    void OnEnable()
    {
        action.Enable();
    }
    void OnDisable()
    {
        action.Disable();
    }
    private void Flip()
    {
        if (movement.horizontalInput > 0)
        {
            sprite.flipX = false;
        }
        else if (movement.horizontalInput < 0)
        {

            sprite.flipX = true;
        }
    }
}
