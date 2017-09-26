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
        //var handTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, Controller);
        //var handGrip = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, Controller);
        touchIndex = OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, Controller);
        touchThumb = OVRInput.Get(OVRInput.Touch.PrimaryThumbRest, Controller);
        //Debug.Log(string.Format("{0}Index:{1} {0}Thumb{2}", hand, touchIndex, touchThumb));
        string poseName;
        if (gripPressed)
        {
            poseName = "Close";
            guestureController.Grab(true);
        }
        else if(triggerPressed)
        {
            poseName = "Point";
            guestureController.Grab(false);
        }
        else if (grabbable)
        {
            poseName = "Grab";
            guestureController.Grab(false);
        }
        else
        {
            poseName = "Open";
        }
            //if (triggerPressed)
            //{
                //if (touchThumb)
                //{
                //    poseName = "Close";
                //}
                //else
                //{
                //    poseName = "ThumbsUp";
                //}

            //}
            //else // not triggerPressed
            //{
        //        if (touchIndex)
        //        {
        //            poseName = "ClosePinch";
        //        }
        //        else // not touchIndex
        //        {
        //            //if (touchThumb)
        //            //{
        //                poseName = "Point";
        //            //}
        //            //else
        //            //{
        //            //    poseName = "Gun";
        //            //}
        //        }
        //    }
        //}
        //else // Not gripPressed
        //{
        //    guestureController.Grab(false);
        //    if (triggerPressed)
        //    {
        //        poseName = "OpenPinch";
        //    }
        //    else // Not triggerPressed
        //    {
        //        if (touchIndex)
        //        {
        //            poseName = "OK";
        //        }
        //        else // Not touchIndex
        //        {
        //            poseName = "Grab";
        //        }
        //    }
        //}

        if (!guestureController.targetPose.Name.Equals(poseName))
        {
            guestureController.SelectPose(poseName);
        }
    }

    private void DoGrabOn(object sender, ControllerInteractionEventArgs e)
    {
        //SelectPose("Close");
        gripPressed = true;
    }

    private void DoGrabOff(object sender, ControllerInteractionEventArgs e)
    {
        //SelectPose("Open");
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
