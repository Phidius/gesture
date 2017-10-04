using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class HandsVRTKController : MonoBehaviour {

    public Hands hand = Hands.Right;
    public bool triggerPressed = false;
    public bool gripPressed = false;
    public bool touchIndex = false;
    public bool touchThumb = false;
    public bool grabbable = false;

    public bool ignoreHardwareInput = false;

    private OVRInput.Controller Controller;
    private HandVRController handVRController;

    public enum Hands
    {
        Right,
        Left
    }

    // Use this for initialization
    void Start () {
        GetComponentInParent<VRTK_InteractGrab>().GrabButtonPressed += DoGrabOn;
        GetComponentInParent<VRTK_InteractGrab>().GrabButtonReleased += DoGrabOff;
        GetComponentInParent<VRTK_InteractUse>().UseButtonPressed += DoUseOn;
        GetComponentInParent<VRTK_InteractUse>().UseButtonReleased += DoUseOff;
        GetComponentInParent<VRTK_InteractTouch>().ControllerTouchInteractableObject += DoStartTouch;
        GetComponentInParent<VRTK_InteractTouch>().ControllerUntouchInteractableObject += DoEndTouch;
        handVRController = GetComponent<HandVRController>();
        if (hand == Hands.Left)
        {
            Controller = OVRInput.Controller.LTouch;
        }
        else
        {
            Controller = OVRInput.Controller.RTouch;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!ignoreHardwareInput)
        {
            HandleHardwareInput();
        }
    }

    private void HandleHardwareInput()
    {
        touchIndex = OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger, Controller);
        touchThumb = OVRInput.Get(OVRInput.Touch.PrimaryThumbRest, Controller);


        if (gripPressed)
        {
            handVRController.fingerSpread = 0;
            handVRController.thumbCurl = .9f;
            handVRController.indexCurl = 1;
            handVRController.middleCurl = 1;
            handVRController.ringCurl = 1;
            handVRController.pinkyCurl = 1;
            handVRController.EnableGrab(true);
        }
        else if (grabbable)
        {
            handVRController.fingerSpread = 1;
            handVRController.thumbCurl = 0;
            handVRController.indexCurl = 0;
            handVRController.middleCurl = 0;
            handVRController.ringCurl = 0;
            handVRController.pinkyCurl = 0;
            handVRController.EnableGrab(false);
        }
        else
        {
            handVRController.fingerSpread = 0;
            handVRController.thumbCurl = .5f;
            handVRController.indexCurl = .2f;
            handVRController.middleCurl = .2f;
            handVRController.ringCurl = .2f;
            handVRController.pinkyCurl = .2f;
            handVRController.EnableGrab(true);
        }
    }
    private void DoGrabOn(object sender, ControllerInteractionEventArgs e)
    {
        gripPressed = true;
    }

    private void DoGrabOff(object sender, ControllerInteractionEventArgs e)
    {
        gripPressed = false;
    }

    private void DoUseOn(object sender, ControllerInteractionEventArgs e)
    {
        triggerPressed = true;
    }

    private void DoUseOff(object sender, ControllerInteractionEventArgs e)
    {
        triggerPressed = false;
    }

    private void DoStartTouch(object sender, ObjectInteractEventArgs e)
    {
        //Debug.Log("DoStartTouch");
        grabbable = true;

    }
    private void DoEndTouch(object sender, ObjectInteractEventArgs e)
    {
        //Debug.Log("DoEndTouch");
        grabbable = false;
    }
}
