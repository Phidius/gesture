using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HandVRController))]
public class HandVRControllerEditor : Editor {

    public override void OnInspectorGUI()
    {
        var guestureController = (HandVRController)target;

        if (DrawDefaultInspector())
        {
            
        }
    }
}
