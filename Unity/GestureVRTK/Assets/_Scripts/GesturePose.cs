using UnityEngine;
[System.Serializable]
public class GesturePose
{
    [SerializeField]
    public string Name;
    [SerializeField]
    public Vector3 RotateThumb; // First element = base of finger, second = middle, third = base
    [SerializeField]
    public Vector3 RotateIndex; // First element = base of finger, second = middle, third = base
    [SerializeField]
    public Vector3 RotateMiddle; // First element = base of finger, second = middle, third = base
    [SerializeField]
    public Vector3 RotateRing; // First element = base of finger, second = middle, third = base
    [SerializeField]
    public Vector3 RotatePinky; // First element = base of finger, second = middle, third = base    
    [SerializeField]
    public float SlideThumb;
    public float SlideIndex;
    public float SlideMiddle;
    public float SlideRing;
    public float SlidePinky;
    [SerializeField]
    public bool ColliderOff = false;
}

