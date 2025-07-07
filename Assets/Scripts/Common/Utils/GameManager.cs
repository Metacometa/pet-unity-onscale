using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Links

    private Player player;

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

        EventManager.OnDeath += RespawnPlayer;
    }

    void OnDestroy()
    {
        EventManager.OnDeath -= RespawnPlayer;
    }

    #endregion

    void RespawnPlayer()
    {
        if (player && checkpoint)
        {
            player.transform.position = checkpoint.transform.position;
        }
    }
}
