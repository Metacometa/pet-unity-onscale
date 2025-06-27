using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnscaleableZone : MonoBehaviour
{
    public bool isScaleable = true;

    [SerializeField] private string targetTag;

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Entered");
        if (collider.CompareTag(targetTag))
        {
            isScaleable = false;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        Debug.Log("Stay");
        if (collider.CompareTag(targetTag))
        {
            isScaleable = false;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        Debug.Log("Exit");
        if (collider.CompareTag(targetTag))
        {
            isScaleable = true;
        }
    }
}
