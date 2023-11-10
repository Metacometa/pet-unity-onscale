using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] private int nextSceneId = 0;
    private Player player;

    public bool isPlayerContained = false;

    [SerializeField] private string targetTag;

    private BoxCollider2D coll;

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        player = FindObjectOfType<Player>();
        Assert.IsNotNull(player);
    }

    private void Update()
    {
        if (player.isLevelEnded == true)
        {
            SceneManager.LoadScene(nextSceneId);
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag(targetTag) && IsContained(collider, coll))
        {
            isPlayerContained = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag(targetTag) && IsContained(collider, coll))
        {
            isPlayerContained = false;
            SceneManager.LoadScene(nextSceneId);
        }
    }
    public bool IsContained(Collider2D maybeSmallerCollider, Collider2D maybeBiggerCollider)
    {
        return maybeSmallerCollider.bounds.max.x <= maybeBiggerCollider.bounds.max.x &&
            maybeSmallerCollider.bounds.max.y <= maybeBiggerCollider.bounds.max.y &&
            maybeSmallerCollider.bounds.min.x >= maybeBiggerCollider.bounds.min.x &&
            maybeSmallerCollider.bounds.min.y >= maybeBiggerCollider.bounds.min.y;
    }
}
