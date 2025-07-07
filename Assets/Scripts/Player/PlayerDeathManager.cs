using UnityEngine;

public class PlayerDeathManager : MonoBehaviour
{
    #region Links

    private Player player;
    private PlayerDash dash;
    private PlayerGravity gravity;

    #endregion

    void Awake()
    {
        dash = GetComponent<PlayerDash>();
        gravity = GetComponent<PlayerGravity>();
    }

    public void ResetPlayer()
    {
        dash?.UpdateDash();
        dash?.TerminateDash();

        gravity?.ResetVelocity();
        gravity?.ResetOrientation();

        Debug.Log("ResetPlayer");
    }
}
