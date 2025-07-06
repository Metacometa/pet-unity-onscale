using System;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    private static EventManager instance;

    #region Events

    //public Action<PlayerContext> GroundingHandler;
    public static event Action OnGrounding;

    //public static UnityEvent<PlayerContext> OnGrounding;


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
}
