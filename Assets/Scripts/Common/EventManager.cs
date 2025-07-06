using System;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    private static EventManager instance;

    #region Events

    public static event Action OnGrounding;
    public static event Action OnGravityGrounding;

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
    }

    public static void TriggerGravityGrounding()
    {
        OnGravityGrounding?.Invoke();
    }
}
