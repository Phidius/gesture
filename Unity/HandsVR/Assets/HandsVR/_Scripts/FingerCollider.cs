using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerCollider : MonoBehaviour {

    public Transform otherName = null;
    public Renderer indicatorRenderer;

    private Collider fingerCollider;

    void Start()
    {
        fingerCollider = gameObject.GetComponent<Collider>();
    }
    
    public void Trigger(bool enable, Color color)
    {
        if (enable)
        {
            if (indicatorRenderer)
            {
                indicatorRenderer.material.color = color;
                //Debug.Log("FingerCollider.Trigger (" + color.ToString() + ")");
            }
        }
        else
        {
            if (indicatorRenderer)
            {
                indicatorRenderer.material.color = Color.white;
                //Debug.Log("FingerCollider.Trigger (White)");
            }
        }
        fingerCollider.isTrigger = enable;
    }
     public bool IsTrigger()
    {
        return fingerCollider.isTrigger;
    }

    void OnTriggerEnter(Collider other)
    {
        //if (other.transform.root.gameObject.name.Equals(transform.root.gameObject.name))
        //{
        //    return;
        //}
        otherName = other.gameObject.transform;
        //Debug.Log(transform.gameObject.name + ":Start touching " + otherName);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform == otherName)
        {
            //Debug.Log(transform.gameObject.name + ":Stop touching " + otherName);
            otherName = null;
            if (indicatorRenderer)
            {
                indicatorRenderer.material.color = Color.grey;
            }
        }
    }
}
