﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandVRController : MonoBehaviour {

    public bool PreventMovement = false;
    [Range(0f, 1f)]
    public float thumbCurl;
    [Range(0f, 1f)]
    public float indexCurl;
    [Range(0f, 1f)]
    public float middleCurl;
    [Range(0f, 1f)]
    public float ringCurl;
    [Range(0f, 1f)]
    public float pinkyCurl;

    [Range(0f, 1f)]
    public float fingerSpread;

    private int smoothness = 7;

    private Transform thumb1;
    private Transform thumb2;
    private Transform thumb3;
    private Transform index1;
    private Transform index2;
    private Transform index3;
    private Transform middle1;
    private Transform middle2;
    private Transform middle3;
    private Transform ring1;
    private Transform ring2;
    private Transform ring3;
    private Transform pinky1;
    private Transform pinky2;
    private Transform pinky3;

    private Collider palmCollider;
    private FingerCollider thumbCollider;
    private FingerCollider indexCollider;
    private FingerCollider middleCollider;
    private FingerCollider ringCollider;
    private FingerCollider pinkyCollider;

    Vector3 rotThumb1 = new Vector3(300f, 220f, 75f);
    Vector3 rotThumb2 = new Vector3(0f, 0f, 0f);
    Vector3 rotThumb3 = new Vector3(0f, 0f, 0f);

    Vector3 rotIndex1 = new Vector3(0f, 350f, 0f);
    Vector3 rotIndex2 = new Vector3(0f, 0f, 0f);
    Vector3 rotIndex3 = new Vector3(0f, 0f, 0f);

    Vector3 rotMiddle1 = new Vector3(0f, 3f, 0f);
    Vector3 rotMiddle2 = new Vector3(0f, 0f, 0f);
    Vector3 rotMiddle3 = new Vector3(0f, 0f, 0f);

    Vector3 rotRing1 = new Vector3(0f, 10f, 0f);
    Vector3 rotRing2 = new Vector3(0f, 0f, 0f);
    Vector3 rotRing3 = new Vector3(0f, 0f, 0f);

    Vector3 rotPinky1 = new Vector3(0f, 20f, 0f);
    Vector3 rotPinky2 = new Vector3(0f, 0f, 0f);
    Vector3 rotPinky3 = new Vector3(0f, 0f, 0f);

    public Vector3 thumbRotation;
    // Use this for initialization
    void Start()
    {
        foreach (var transform in gameObject.GetComponentsInChildren<Transform>())
        {
            switch (transform.name)
            {
                case "Palm_R":
                    palmCollider = transform.GetComponent<Collider>();
                    break;
                case "Thumb_1":
                    thumb1 = transform;
                    break;

                case "Thumb_2":
                    thumb2 = transform;
                    break;

                case "Thumb_3":
                    thumb3 = transform;
                    thumbCollider = transform.GetComponent<FingerCollider>();
                    break;

                case "Index_1":
                    index1 = transform;
                    break;

                case "Index_2":
                    index2 = transform;
                    break;

                case "Index_3":
                    index3 = transform;
                    indexCollider = transform.GetComponent<FingerCollider>();
                    break;

                case "Middle_1":
                    middle1 = transform;
                    break;

                case "Middle_2":
                    middle2 = transform;
                    break;

                case "Middle_3":
                    middle3 = transform;
                    middleCollider = transform.GetComponent<FingerCollider>();
                    break;

                case "Ring_1":
                    ring1 = transform;
                    break;

                case "Ring_2":
                    ring2 = transform;
                    break;

                case "Ring_3":
                    ring3 = transform;
                    ringCollider = transform.GetComponent<FingerCollider>();
                    break;

                case "Pinky_1":
                    pinky1 = transform;
                    break;

                case "Pinky_2":
                    pinky2 = transform;
                    break;

                case "Pinky_3":
                    pinky3 = transform;
                    pinkyCollider = transform.GetComponent<FingerCollider>();
                    break;

            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!PreventMovement)
        {
            Movement();
        }
        
    }

    private void Movement()
    {
        var lastThumb1 = new Vector3(rotThumb1.x, rotThumb1.y, rotThumb1.z);
        var lastThumb2 = new Vector3(rotThumb2.x, rotThumb2.y, rotThumb2.z);
        var lastThumb3 = new Vector3(rotThumb3.x, rotThumb3.y, rotThumb3.z);

        var lastIndex1 = new Vector3(rotIndex1.x, rotIndex1.y, rotIndex1.z);
        var lastIndex2 = new Vector3(rotIndex2.x, rotIndex2.y, rotIndex2.z);
        var lastIndex3 = new Vector3(rotIndex3.x, rotIndex3.y, rotIndex3.z);

        var lastMiddle1 = new Vector3(rotMiddle1.x, rotMiddle1.y, rotMiddle1.z);
        var lastMiddle2 = new Vector3(rotMiddle2.x, rotMiddle2.y, rotMiddle2.z);
        var lastMiddle3 = new Vector3(rotMiddle3.x, rotMiddle3.y, rotMiddle3.z);

        var lastRing1 = new Vector3(rotRing1.x, rotRing1.y, rotRing1.z);
        var lastRing2 = new Vector3(rotRing2.x, rotRing2.y, rotRing2.z);
        var lastRing3 = new Vector3(rotRing3.x, rotRing3.y, rotRing3.z);

        var lastPinky1 = new Vector3(rotPinky1.x, rotPinky1.y, rotPinky1.z);
        var lastPinky2 = new Vector3(rotPinky2.x, rotPinky2.y, rotPinky2.z);
        var lastPinky3 = new Vector3(rotPinky3.x, rotPinky3.y, rotPinky3.z);
        
        rotThumb1.z = Mathf.Lerp(rotThumb1.z, 75 + (70 * thumbCurl), Time.deltaTime * smoothness);
        rotThumb2.z = Mathf.Lerp(rotThumb2.z, 10 * thumbCurl, Time.deltaTime * smoothness);
        rotThumb3.z = Mathf.Lerp(rotThumb3.z, 40 * thumbCurl, Time.deltaTime * smoothness);
        thumb1.localEulerAngles = rotThumb1;
        thumb2.localEulerAngles = rotThumb2;
        thumb3.localEulerAngles = rotThumb3;

        if (thumbCollider != null && !thumbCollider.otherName.Equals(string.Empty))
        {
            rotThumb1 = new Vector3(lastThumb1.x, lastThumb1.y, lastThumb1.z);
            rotThumb2 = new Vector3(lastThumb2.x, lastThumb2.y, lastThumb2.z);
            rotThumb3 = new Vector3(lastThumb3.x, lastThumb3.y, lastThumb3.z);

            thumb1.localEulerAngles = rotThumb1;
            thumb2.localEulerAngles = rotThumb2;
            thumb3.localEulerAngles = rotThumb3;
        }

        thumbRotation.x = rotThumb1.x;
        thumbRotation.y = rotThumb1.y;
        thumbRotation.z = rotThumb1.z;

        rotIndex1.z = Mathf.Lerp(rotIndex1.z, 90 * indexCurl, Time.deltaTime * smoothness);
        rotIndex1.y = Mathf.Lerp(rotIndex1.y, 360 + (-10 * fingerSpread), Time.deltaTime * smoothness);
        rotIndex2.z = Mathf.Lerp(rotIndex2.z, 90 * indexCurl, Time.deltaTime * smoothness);
        rotIndex3.z = Mathf.Lerp(rotIndex3.z, 90 * indexCurl, Time.deltaTime * smoothness);

        index1.localEulerAngles = rotIndex1;
        index2.localEulerAngles = rotIndex2;
        index3.localEulerAngles = rotIndex3;
        if (indexCollider != null && !indexCollider.otherName.Equals(""))
        {
            // Move back to before the collision
            rotIndex1 = new Vector3(lastIndex1.x, lastIndex1.y, lastIndex1.z);
            rotIndex2 = new Vector3(lastIndex2.x, lastIndex2.y, lastIndex2.z);
            rotIndex3 = new Vector3(lastIndex3.x, lastIndex3.y, lastIndex3.z);
            index1.localEulerAngles = rotIndex1;
            index2.localEulerAngles = rotIndex2;
            index3.localEulerAngles = rotIndex3;
        }

        rotMiddle1.z = Mathf.Lerp(rotMiddle1.z, 90 * middleCurl, Time.deltaTime * smoothness);
        rotMiddle1.y = Mathf.Lerp(rotMiddle1.y, (3 * fingerSpread), Time.deltaTime * smoothness);
        rotMiddle2.z = Mathf.Lerp(rotMiddle2.z, 90 * middleCurl, Time.deltaTime * smoothness);
        rotMiddle3.z = Mathf.Lerp(rotMiddle3.z, 90 * middleCurl, Time.deltaTime * smoothness);
        middle1.localEulerAngles = rotMiddle1;
        middle2.localEulerAngles = rotMiddle2;
        middle3.localEulerAngles = rotMiddle3;
        if (middleCollider != null && !middleCollider.otherName.Equals(""))
        {
            rotMiddle1 = new Vector3(lastMiddle1.x, lastMiddle1.y, lastMiddle1.z);
            rotMiddle2 = new Vector3(lastMiddle2.x, lastMiddle2.y, lastMiddle2.z);
            rotMiddle3 = new Vector3(lastMiddle3.x, lastMiddle3.y, lastMiddle3.z);
            middle1.localEulerAngles = rotMiddle1;
            middle2.localEulerAngles = rotMiddle2;
            middle3.localEulerAngles = rotMiddle3;
        }

        rotRing1.z = Mathf.Lerp(rotRing1.z, 90 * ringCurl, Time.deltaTime * smoothness);
        rotRing1.y = Mathf.Lerp(rotRing1.y, (15f * fingerSpread), Time.deltaTime * smoothness);
        rotRing2.z = Mathf.Lerp(rotRing2.z, 90 * ringCurl, Time.deltaTime * smoothness);
        rotRing3.z = Mathf.Lerp(rotRing3.z, 90 * ringCurl, Time.deltaTime * smoothness);
        ring1.localEulerAngles = rotRing1;
        ring2.localEulerAngles = rotRing2;
        ring3.localEulerAngles = rotRing3;

        if (ringCollider != null && !ringCollider.otherName.Equals(""))
        {
            rotRing1 = new Vector3(lastRing1.x, lastRing1.y, lastRing1.z);
            rotRing2 = new Vector3(lastRing2.x, lastRing2.y, lastRing2.z);
            rotRing3 = new Vector3(lastRing3.x, lastRing3.y, lastRing3.z);
            ring1.localEulerAngles = rotRing1;
            ring2.localEulerAngles = rotRing2;
            ring3.localEulerAngles = rotRing3;
        }

        rotPinky1.z = Mathf.Lerp(rotPinky1.z, 90 * pinkyCurl, Time.deltaTime * smoothness);
        rotPinky1.y = Mathf.Lerp(rotPinky1.y, (20 * fingerSpread), Time.deltaTime * smoothness);
        rotPinky2.z = Mathf.Lerp(rotPinky2.z, 90 * pinkyCurl, Time.deltaTime * smoothness);
        rotPinky3.z = Mathf.Lerp(rotPinky3.z, 90 * pinkyCurl, Time.deltaTime * smoothness);
        pinky1.localEulerAngles = rotPinky1;
        pinky2.localEulerAngles = rotPinky2;
        pinky3.localEulerAngles = rotPinky3;
        if (pinkyCollider != null && !pinkyCollider.otherName.Equals(""))
        {
            rotPinky1 = new Vector3(lastPinky1.x, lastPinky1.y, lastPinky1.z);
            rotPinky2 = new Vector3(lastPinky2.x, lastPinky2.y, lastPinky2.z);
            rotPinky3 = new Vector3(lastPinky3.x, lastPinky3.y, lastPinky3.z);
            pinky1.localEulerAngles = rotPinky1;
            pinky2.localEulerAngles = rotPinky2;
            pinky3.localEulerAngles = rotPinky3;
        }
    }
    public void EnableGrab(bool enable)
    {

        palmCollider.isTrigger = enable;
        thumbCollider.Touch(enable);
        indexCollider.Touch(enable);
        middleCollider.Touch(enable);
        ringCollider.Touch(enable);
        pinkyCollider.Touch(enable);
    }
}
