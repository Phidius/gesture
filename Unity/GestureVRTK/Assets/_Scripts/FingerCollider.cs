using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerCollider : MonoBehaviour {

    public string otherName = string.Empty;
    private CapsuleCollider collider;

    void Start()
    {
        collider = gameObject.GetComponent<CapsuleCollider>();
    }

    public void Touch(bool enable)
    {
        collider.isTrigger = enable;
    }

    void OnTriggerEnter(Collider other)
    {
        otherName = other.gameObject.name;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == otherName)
        {
            otherName = string.Empty;
        }
    }
}
