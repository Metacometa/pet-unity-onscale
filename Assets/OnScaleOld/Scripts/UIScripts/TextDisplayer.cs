using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplayer : MonoBehaviour
{
    [SerializeField] private string playerTag;
    [SerializeField] private string newText;

    [SerializeField] private Text text;

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.CompareTag(playerTag)) 
        {
            text.text = newText;
        }
    }

    void OnTriggerExit2D(Collider2D coll) 
    {
        if (coll.CompareTag(playerTag)) 
        {
            text.text = "";
        }
    }
}
