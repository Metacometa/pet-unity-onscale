using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private string targetTag;
    private CompositeCollider2D col;

    private Player player;
    private void Start()
    {
        col = GetComponent<CompositeCollider2D>();
        player = GameObject.FindWithTag(targetTag).GetComponent<Player>();
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.CompareTag(targetTag) && IsContained(coll, col) && !player.isEndingAnimation)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public bool IsContained(Collider2D maybeSmallerCollider, Collider2D maybeBiggerCollider)
    {
        return maybeSmallerCollider.bounds.max.x <= maybeBiggerCollider.bounds.max.x &&
            maybeSmallerCollider.bounds.min.x >= maybeBiggerCollider.bounds.min.x &&
            maybeSmallerCollider.bounds.max.y <= maybeBiggerCollider.bounds.max.y &&
            maybeSmallerCollider.bounds.min.y >= maybeBiggerCollider.bounds.min.y;
    }
}
