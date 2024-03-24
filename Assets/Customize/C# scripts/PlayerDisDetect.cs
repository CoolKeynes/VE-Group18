using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using Ubiq.Rooms;
using UMA.PoseTools;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using Ubiq.Spawning;

public class PlayerDisDetect : MonoBehaviour
{

    // Start is called before the first frame update
    public Transform PushPoint;
    public Transform InsideChestPoint;
    public Transform PlayerLeftHand;
    public Transform PlayerRightHand;
    public Transform RightChestTransform;
    public Transform LeftChestTransform;
    public Transform HeadPoint;
    public Transform JawPoint;
    public UMAExpressionPlayer expressionPlayer;
    private Vector3 originalRightChestPosition;  // right chest position
    private Vector3 originalLeftChestPosition;  // left chest position
    private InputDevice leftHandDevice;
    private InputDevice rightHandDevice;
    public float openThreshold = 0.2f; // open mouth
    public float farThreshold = 0.25f; // close mouth 
    bool isMouthOpen = false; 
    bool handWasFar = true;  
    bool isCompressing = false; // determine if you are doing chest compression, false 
    int press_time = 0;
    NetworkContext context;
    //Vector3 LeftControllerVelocity;
    //Vector3 RightControllerVelocity;
    void Start()
    {
        context = NetworkScene.Register(this);
        originalRightChestPosition = RightChestTransform.localPosition;
        originalLeftChestPosition = LeftChestTransform.localPosition;
        leftHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        rightHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

    }
    private struct Message
    {
        public Vector3 RightChestTransform;
        public Vector3 LeftChestTransform;
        public bool MouthOpen;
        //public bool isCompressing;
    }
    // Update is called once per frame
    void Update()
    {
        expressionPlayer.overrideMecanimJaw = true;
        if (!leftHandDevice.isValid || !rightHandDevice.isValid)
        {
            leftHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            rightHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        }
        //leftHandDevice.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
        if (leftHandDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 leftVelocity))
        {
            //Debug.Log("Left Hand Velocity: " + leftVelocity);
        }
        if (rightHandDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 rightVelocity))
        {
            //Debug.Log("Left Hand Velocity: " + rightVelocity);
        }
        // Chest compressions action
        if (Vector3.Distance(PushPoint.position, PlayerLeftHand.position) < 0.1f || Vector3.Distance(PushPoint.position, PlayerRightHand.position) < 0.1f)
        { // 


            //if(RightChestTransform.position.y < )
            // Set a minimum height for the Y position of the chest points   
            //Debug.Log(LeftChestTransform.position.y); //0.19
            isCompressing = true;
            if (RightChestTransform.position.y > 0.007 || LeftChestTransform.position.y > 0.01)
            {
                Debug.Log("Hit chest");
                RightChestTransform.position -= -Vector3.down * (leftVelocity.y + rightVelocity.y) / 2 * 0.01f; 
                LeftChestTransform.position -= -Vector3.down * (leftVelocity.y + rightVelocity.y) / 2 * 0.01f;  
                Message m = new Message();
                m.RightChestTransform = RightChestTransform.position;
                m.LeftChestTransform = LeftChestTransform.position;
                //m.isCompressing = false; 
                context.SendJson(m);
                //Debug.Log(position); 
            }
            else
            {
                press_time += 1;
                Debug.Log("Hit chest" + press_time);
            }
        }
        else if (isCompressing)
        {
            //  move chest back to original postion 
            RightChestTransform.localPosition = originalRightChestPosition;
            LeftChestTransform.localPosition = originalLeftChestPosition;
            isCompressing = false;
            Message m = new Message();
            m.RightChestTransform = RightChestTransform.position;
            m.LeftChestTransform = LeftChestTransform.position;
        }

        JawUpdate();
    }
    void JawUpdate()
    {
        // Open jaw action

        float RightToJaw = Vector3.Distance(JawPoint.position, PlayerRightHand.position);
        float LeftToJaw = Vector3.Distance(JawPoint.position, PlayerLeftHand.position);

        float LeftToHand = Vector3.Distance(HeadPoint.position, PlayerLeftHand.position);
        float RightToHand = Vector3.Distance(HeadPoint.position, PlayerRightHand.position);
        if (((RightToJaw < openThreshold && LeftToHand < openThreshold) || (LeftToJaw < openThreshold && RightToHand < openThreshold)) && handWasFar)
        {
            // open Jaw
            isMouthOpen = !isMouthOpen;
            // determine whether hands is away   
            handWasFar = false;

            // open Jaw 
            if (isMouthOpen)
            {
                Debug.Log("Open Jaw");
                expressionPlayer.jawOpen_Close = 1.0f;
                isMouthOpen = true;
                Message m = new Message();
                m.MouthOpen = true;
                context.SendJson(m);
            }
            else
            {
                Debug.Log("Close Jaw");
                expressionPlayer.jawOpen_Close = -1.0f;
                isMouthOpen = false;
                Message m = new Message();
                m.MouthOpen = false;
                context.SendJson(m);
            }
        }
        else if (RightToJaw > farThreshold || LeftToHand > farThreshold || LeftToJaw > farThreshold || RightToHand > farThreshold)
        {
            // Update the status of the hand to indicate that the hand is away
            handWasFar = true;
        }
    }
    public void ProcessMessage(ReferenceCountedSceneGraphMessage m)
    {
        // Parse the message
        var message = m.FromJson<Message>();
        //isCompressing = message.isLearner;
        RightChestTransform.position = message.RightChestTransform;
        LeftChestTransform.position = message.LeftChestTransform;
        Debug.Log("RightChestTransform" + message.RightChestTransform);
        if (message.MouthOpen == true)
        {
            expressionPlayer.jawOpen_Close = 1.0f;
            Debug.Log("Receive mouth open");
        }
        else if (message.MouthOpen == false)
        {
            expressionPlayer.jawOpen_Close = -1.0f;
            Debug.Log("Receive mouth close");
        }

        //Debug.Log(gameObject.name + "updated");
    }
}


