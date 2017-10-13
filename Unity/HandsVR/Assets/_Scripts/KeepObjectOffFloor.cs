using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepObjectOffFloor : MonoBehaviour {

    Vector3 originalLocation;
	// Use this for initialization
	void Start () {
        originalLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {

		if (transform.position.y <= 1f)
        {
            Debug.Log("Root: "+ transform.root.name);
            Debug.Log("KeepObjectOffFloor - move " + gameObject.name + " off the floor, back to the original position!");
            transform.position = originalLocation;
        }
	}
}
