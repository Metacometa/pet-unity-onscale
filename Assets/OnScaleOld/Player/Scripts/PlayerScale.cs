using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerScale : MonoBehaviour
{
    public float minScale = 0.2f;
    public float maxScale = 1.5f;
    [SerializeField] private float scaleMultiplier = 1;
    [SerializeField] private float exitingScaleMultiplier = 0.7f;

    [SerializeField] private PlayerMovement movement;

    private Rigidbody2D rb;
    private Collider2D coll;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void ScaleByMoving()
    {
        if (movement.horizontalInput != 0) 
        {
            Scale(scaleMultiplier * movement.horizontalInput, minScale, maxScale);
        }
    }

    public void ExitingScaling()
    {
        Scale(-exitingScaleMultiplier, 0, maxScale);
    }

    public void Scale(float scaleMagnitude, float minValue, float maxValue)
    {
        float scaleDiff = scaleMagnitude * Time.deltaTime;
        float newScale = rb.transform.localScale.y + scaleDiff;

        if (newScale >= minValue && newScale <= maxValue)
        {
            if (IsDiminishing(scaleMagnitude) || !PlayerIsPressed(Time.deltaTime * 2))
            {
                rb.transform.localScale += new Vector3(scaleDiff, scaleDiff, 0);
                rb.position = new Vector3(rb.position.x, rb.position.y + scaleDiff, 0);
            }
        }
    }

    private bool PlayerIsPressed(float scaleIncrement)
    {
        return IsPressed(coll.bounds.center, coll.bounds.size, Vector2.up, 0, scaleIncrement, "Obstacle")
        || (IsPressed(coll.bounds.center, coll.bounds.size, Vector2.left, 0, scaleIncrement, "Obstacle") &&
        IsPressed(coll.bounds.center, coll.bounds.size, Vector2.right, 0, scaleIncrement, "Obstacle"));
    }

    private bool IsDiminishing(float dir)
    {
        return dir < 0;
    }

    private bool IsPressed(Vector2 origin, Vector2 size, Vector2 dir, float angle, float distance, string maskName)
    {
        RaycastHit2D hit = Physics2D.BoxCast(origin, size, angle, dir, distance, LayerMask.GetMask(maskName));
        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}