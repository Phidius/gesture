using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class RingWearManager : MonoBehaviour {

    public VRTK_Pointer pointer;

	// Use this for initialization
	void Start () {
        var snapEvents = GetComponent<VRTK.UnityEventHelper.VRTK_SnapDropZone_UnityEvents>();
        if (snapEvents == null)
        {
            snapEvents = gameObject.AddComponent<VRTK.UnityEventHelper.VRTK_SnapDropZone_UnityEvents>();
        }
        pointer = GetComponentInParent<VRTK_Pointer>();
        snapEvents.OnObjectSnappedToDropZone.AddListener(ObjectSnapped);
        snapEvents.OnObjectUnsnappedFromDropZone.AddListener(ObjectUnsnapped);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ObjectSnapped(object sender, SnapDropZoneEventArgs e)
    {
        //Debug.Log(e.snappedObject.name + " snapped to " + transform.parent.parent.parent.parent.name);
        pointer.enableTeleport = true;
    }
    public void ObjectUnsnapped(object sender, SnapDropZoneEventArgs e)
    {
        //Debug.Log("Snapped Off: " + e.snappedObject.name + transform.parent.parent.parent.parent.name);
        pointer.enableTeleport = false;
    }
}
