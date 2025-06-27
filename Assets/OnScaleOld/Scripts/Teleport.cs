using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private Vector3 exitPosition;

    [SerializeField] private GameObject exitGameObject;
    [SerializeField] private string playerTag;

    void Awake() {
        exitPosition = exitGameObject.transform.position;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.CompareTag(playerTag)) {
            coll.transform.position = exitPosition;
        }
    }
}
