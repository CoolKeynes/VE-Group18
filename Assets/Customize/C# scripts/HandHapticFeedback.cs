using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandHapticFeedback : MonoBehaviour
{
    public Transform LeftHand;
    public Transform RightHand;
    public Transform InsideChestPoint;
    public Transform LeftShoulder;
    public Transform RightShoulder;
    public Transform MouthPoint;
    public float triggerDistance = 0.5f;
    private InputDevice RightController;
    private InputDevice LeftController;
    private InputDevice headDevice;
    private Vector3  headsetPosition;
    void Start()
    {
        // obtain the devices 
        RightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        LeftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        headDevice = InputDevices.GetDeviceAtXRNode(XRNode.Head);
    }

    void Update()
    {
        if (!RightController.isValid)
        {
            RightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        }
        if (!LeftController.isValid)
        {
            LeftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        }
        // Get the position of the handle
        if (Vector3.Distance(LeftHand.position, InsideChestPoint.position) < triggerDistance && Vector3.Distance(RightHand.position, InsideChestPoint.position) < triggerDistance)
        {
            Debug.Log("chest vibrate");
            HapticFeedback();
        }
        if (Vector3.Distance(LeftHand.position, LeftShoulder.position) < 0.2 && Vector3.Distance(RightHand.position, RightShoulder.position) < 0.2)
        {
            Debug.Log("shoulder vibrate");
            HapticFeedback();
        }


        // Check whether the device works
        if (headDevice.isValid)
        {
            // Gets the position and rotation of the headset
            if (headDevice.TryGetFeatureValue(CommonUsages.devicePosition, out headsetPosition) && Vector3.Distance(headsetPosition, MouthPoint.position) < 0.2)
            {
                Debug.Log("breath vibrate");
                HapticFeedback();
            }
        }
    }
    void HapticFeedback()
    {
        // Trigger a vibration
        RightController.SendHapticImpulse(0, 1.0f, 0.1f);
        LeftController.SendHapticImpulse(0, 1.0f, 0.1f);
    }
}
