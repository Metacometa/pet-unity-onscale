using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Killer : MonoBehaviour
{
    [SerializeField] private string target;
    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.CompareTag(target)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
