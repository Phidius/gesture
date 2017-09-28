using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class UserController : MonoBehaviour {

    public Hands hand = Hands.Right;
    public bool triggerPressed = false;
    public bool gripPressed = false;
    public bool touchIndex = false;
    public bool touchThumb = false;
    public bool grabbable = false;

    public OVRInput.Controller Controller;
    public GestureController guestureController;

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
        guestureController = GetComponent<GestureController>();
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
        touchIndex = OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, Controller);
        touchThumb = OVRInput.Get(OVRInput.Touch.PrimaryThumbRest, Controller);

        string poseName;
        if (gripPressed)
        {
            poseName = "Close";
        }
        else if(triggerPressed)
        {
            poseName = "Point";
        }
        else if (grabbable)
        {
            poseName = "Spread";
        }
        else
        {
            poseName = "Idle";
        }
        
        if (!guestureController.targetPose.Name.Equals(poseName))
        {
            guestureController.SelectPose(poseName);
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
        grabbable = true;

    }
    private void DoEndTouch(object sender, ObjectInteractEventArgs e)
    {
        grabbable = false;
    }
}
