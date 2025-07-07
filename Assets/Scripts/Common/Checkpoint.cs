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

        //EventManager.OnDeath += SetOrientationToPlayer;
    }

    void OnDestroy()
    {
        //EventManager.OnDeath -= SetOrientationToPlayer;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("Checkpoint Trigger");
            //Debug.Log($"On trigger speed: {}")
            playerContext.Orientation = checkpointOrientation;
            //checkpointOrientation = playerContext.Orientation;
        }
    }

/*    void SetOrientationToPlayer()
    {
        playerContext.Orientation = checkpointOrientation;
    }*/
}
