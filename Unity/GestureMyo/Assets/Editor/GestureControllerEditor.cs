using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GestureController))]
public class GuestureControllerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        var guestureController = (GestureController)target;

        if (DrawDefaultInspector())
        {
            guestureController.SelectPose(guestureController.selectedPose);
            guestureController.ApplyPose();
        }
    }
}