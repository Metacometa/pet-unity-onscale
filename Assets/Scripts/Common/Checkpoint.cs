using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Vector2 checkpointOrientation;

    private Player player;
    private PlayerContext playerContext;

    void Awake()
    {
        player = FindFirstObjectByType<Player>();
        playerContext = player?.context;
    }

    void OnDestroy()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            playerContext.Orientation = checkpointOrientation;
        }
    }

}
