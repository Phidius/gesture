using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerCollider : MonoBehaviour {

    public string colliderName = string.Empty;

    void OnTriggerEnter(Collider other)
    {
        colliderName = other.gameObject.name;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == colliderName)
        {
            colliderName = string.Empty;
        }
    }
}
