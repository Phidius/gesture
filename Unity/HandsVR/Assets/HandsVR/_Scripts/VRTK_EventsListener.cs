using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VRTK_EventsListener : MonoBehaviour {

    private HandVRController handVRController;
    private float thumbRest = .75f;
    private float indexRest = .25f;
    private float middleRest = .3f;
    private float ringRest = .35f;
    private float pinkyRest = .4f;

    private bool thumbTouch = false;
    private float gripIndex = 0f;

    // Use this for initialization
    void Start () {

        var controllerEvents = GetComponentInParent<VRTK_ControllerEvents>();
        handVRController = GetComponent<HandVRController>();

        // Register events to detect when close enough to grab
        GetComponentInParent<VRTK_InteractTouch>().ControllerTouchInteractableObject += DoStartTouch;
        GetComponentInParent<VRTK_InteractTouch>().ControllerUntouchInteractableObject += DoEndTouch;

        controllerEvents.TriggerTouchStart += new ControllerInteractionEventHandler(DoTriggerTouchStart);
        controllerEvents.TriggerTouchEnd += new ControllerInteractionEventHandler(DoTriggerTouchEnd);
        controllerEvents.TriggerAxisChanged += new ControllerInteractionEventHandler(DoTriggerAxisChanged);
        
        controllerEvents.GripAxisChanged += new ControllerInteractionEventHandler(DoGripAxisChanged);

        controllerEvents.TouchpadPressed += new ControllerInteractionEventHandler(DoTouchpadPressed);
        controllerEvents.TouchpadReleased += new ControllerInteractionEventHandler(DoTouchpadReleased);
        
        controllerEvents.ButtonOneTouchStart += new ControllerInteractionEventHandler(DoButtonOneTouchStart);
        controllerEvents.ButtonOneTouchEnd += new ControllerInteractionEventHandler(DoButtonOneTouchEnd);
        controllerEvents.ButtonTwoTouchStart += new ControllerInteractionEventHandler(DoButtonTwoTouchStart);
        controllerEvents.ButtonTwoTouchEnd += new ControllerInteractionEventHandler(DoButtonTwoTouchEnd);

        controllerEvents.TouchpadAxisChanged += new ControllerInteractionEventHandler(DoTouchpadAxisChanged);

        handVRController.fingerSpread = 0f;
        handVRController.thumbCurl = thumbRest;
        handVRController.indexCurl = indexRest;
        handVRController.middleCurl = middleRest;
        handVRController.ringCurl = ringRest;
        handVRController.pinkyCurl = pinkyRest;
    }

    private void DoStartTouch(object sender, ObjectInteractEventArgs e)
    {
        // Spread the fingers when a pickup is available to pickup items better
        if (gripIndex < 0.05f)
        {
            handVRController.fingerSpread = 1f;

            handVRController.thumbCurl = 0f;
            handVRController.indexCurl = 0f;
            handVRController.middleCurl = 0f;
            handVRController.ringCurl = 0f;
            handVRController.pinkyCurl = 0f;
        }


    }
    private void DoEndTouch(object sender, ObjectInteractEventArgs e)
    {
        if (gripIndex < .05f)
        {
            // Return fingers to their default orientation when a pickup is no longer available
            handVRController.fingerSpread = 0f;
            handVRController.thumbCurl = thumbRest;
            handVRController.indexCurl = indexRest;
            handVRController.middleCurl = middleRest;
            handVRController.ringCurl = ringRest;
            handVRController.pinkyCurl = pinkyRest;
        }

    }
    private void DoTriggerTouchStart(object sender, ControllerInteractionEventArgs e)
    {
        // Start touching the trigger (proximity)
        handVRController.indexCurl = .3f;
    }

    private void DoTriggerTouchEnd(object sender, ControllerInteractionEventArgs e)
    {
        // Stop touching the trigger (proximity)
        handVRController.indexCurl = .0f;
    }

    private void DoTriggerAxisChanged(object sender, ControllerInteractionEventArgs e)
    {
        handVRController.indexCurl = .3f + (.7f * e.buttonPressure);
    }

    private void DoGripAxisChanged(object sender, ControllerInteractionEventArgs e)
    {
        //DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "axis changed", e);
        
        gripIndex = e.buttonPressure;
        if (thumbTouch)
        {
            // Move the thumb with the grip fingers when the thumb isn't touching a button
            handVRController.thumbCurl = thumbRest + ((1f - thumbRest) * gripIndex);
        }
        handVRController.middleCurl = middleRest + ((1f - middleRest) * gripIndex);
        handVRController.ringCurl = ringRest + ((1f - ringRest) * gripIndex);
        handVRController.pinkyCurl = pinkyRest + ((1f - pinkyRest) * gripIndex);
    }

    private void DoButtonOneTouchStart(object sender, ControllerInteractionEventArgs e)
    {
        // Move the thumb with the grip fingers when the thumb isn't touching a button
        handVRController.thumbCurl = thumbRest + ((1f - thumbRest) * gripIndex);
        thumbTouch = true;
        //DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON ONE", "touched", e);
    }

    private void DoButtonOneTouchEnd(object sender, ControllerInteractionEventArgs e)
    {
        handVRController.thumbCurl = 0f;
        thumbTouch = false;
        
        //DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON ONE", "untouched", e);
    }
    private void DoButtonTwoTouchStart(object sender, ControllerInteractionEventArgs e)
    {
        // Move the thumb with the grip fingers when the thumb isn't touching a button
        handVRController.thumbCurl = thumbRest + ((1f - thumbRest) * gripIndex);
        thumbTouch = true;
        //DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON TWO", "touched", e);
    }

    private void DoButtonTwoTouchEnd(object sender, ControllerInteractionEventArgs e)
    {
        handVRController.thumbCurl = 0f;
        thumbTouch = false;
        //DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON TWO", "untouched", e);
    }

    private void DoTouchpadPressed(object sender, ControllerInteractionEventArgs e)
    {
        // Move the thumb with the grip fingers when the thumb isn't touching a button
        handVRController.thumbCurl = thumbRest + ((1f - thumbRest) * gripIndex);
        thumbTouch = true;
        //DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TOUCHPAD", "pressed down", e);
    }

    private void DoTouchpadReleased(object sender, ControllerInteractionEventArgs e)
    {
        thumbTouch = false;
        handVRController.thumbCurl = 0f;
    }


    private void DoTouchpadAxisChanged(object sender, ControllerInteractionEventArgs e)
    {
        //DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TOUCHPAD", "axis changed", e);
    }

    private void DebugLogger(uint index, string button, string action, ControllerInteractionEventArgs e)
    {
        VRTK_Logger.Info("Controller on index '" + index + "' " + button + " has been " + action
                + " with a pressure of " + e.buttonPressure + " / trackpad axis at: " + e.touchpadAxis + " (" + e.touchpadAngle + " degrees)");
    }
}
