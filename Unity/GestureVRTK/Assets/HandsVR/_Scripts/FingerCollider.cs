using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerCollider : MonoBehaviour {

    public string otherName = string.Empty;
    private Collider fingerCollider;

    void Start()
    {
        fingerCollider = gameObject.GetComponent<Collider>();
    }

    public void Touch(bool enable)
    {
        fingerCollider.isTrigger = enable;
        if (!enable) otherName = string.Empty;
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    otherName = other.gameObject.name;
    //}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            otherName = other.gameObject.name;
            Debug.Log(transform.gameObject.name + ":Start touching " + otherName);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == otherName)
        {
            Debug.Log(transform.gameObject.name + ":Stop touching " + otherName);
            otherName = string.Empty;
        }
    }
}
