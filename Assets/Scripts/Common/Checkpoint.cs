using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Vector2 checkpointOrientation;

    private Player player;
    private PlayerContext playerContext;

    private Collider2D colliler2D;

    void Awake()
    {
        player = FindFirstObjectByType<Player>();
        playerContext = player?.context;

        colliler2D = GetComponent<Collider2D>();
    }

    void OnDestroy()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            playerContext.Orientation = checkpointOrientation;

            colliler2D.enabled = false;
        }
    }

}
