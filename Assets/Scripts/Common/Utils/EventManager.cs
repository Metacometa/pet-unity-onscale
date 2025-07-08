using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private static EventManager instance;

    #region Events

    public static event Action OnGrounding;
    public static event Action OnGravityColliding;

    public static event Action OnDeath;

    #endregion

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
    }

    public static void TriggerGrounding()
    {
        OnGrounding?.Invoke();
        Debug.Log("TriggerGrounding");
    }

    public static void TriggerGravityColliding()
    {
        OnGravityColliding?.Invoke();
        Debug.Log("TriggerGravityColliding");
    }

    public static void TriggerDeath()
    {
        OnDeath?.Invoke();
    }
}
