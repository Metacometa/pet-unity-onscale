using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Links

    private Player player;
    [SerializeField] private PlayerDeathManager deathManager;

    #endregion

    #region Private Variables

    private static GameManager instance;
    [SerializeField] public Checkpoint checkpoint;

    #endregion

    #region Callbacks

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        player = FindFirstObjectByType<Player>();
        deathManager = player?.GetComponent<PlayerDeathManager>();

        EventManager.OnDeath += RespawnPlayer;
    }

    void OnDestroy()
    {
        EventManager.OnDeath -= RespawnPlayer;
    }

    #endregion

    void RespawnPlayer()
    {
        deathManager?.ResetPlayer();

        if (player && checkpoint)
        {
            Debug.Log("RespawnPlayer");
            player.transform.position = checkpoint.transform.position;         
        }
    }
}
