using UnityEngine;

public class ExitDeathArea : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            EventManager.TriggerDeath();
        }
    }
}
