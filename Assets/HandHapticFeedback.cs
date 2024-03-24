using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandHapticFeedback : MonoBehaviour
{
    public Transform LeftHand;
    public Transform RightHand;
    public Transform InsideChestPoint; // Ŀ���
    public Transform LeftShoulder;
    public Transform RightShoulder;
    public Transform MouthPoint;
    public float triggerDistance = 0.5f; // �����𶯵ľ���
    private InputDevice RightController;
    private InputDevice LeftController;
    private InputDevice headDevice;
    private Vector3  headsetPosition;
    void Start()
    {
        // ��ȡ�ֱ��豸������������Ϊ��
        RightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        LeftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        headDevice = InputDevices.GetDeviceAtXRNode(XRNode.Head);
    }

    void Update()
    {
        // ����豸�Ƿ���Ч
        if (!RightController.isValid)
        {
            RightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        }
        if (!LeftController.isValid)
        {
            LeftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        }
        // ��ȡ�ֱ���λ��
        if (Vector3.Distance(LeftHand.position, InsideChestPoint.position) < triggerDistance && Vector3.Distance(RightHand.position, InsideChestPoint.position) < triggerDistance)
        {
            // ������
            Debug.Log("chest vibrate");
            HapticFeedback();
        }
        if (Vector3.Distance(LeftHand.position, LeftShoulder.position) < 0.2 && Vector3.Distance(RightHand.position, RightShoulder.position) < 0.2)
        {
            // ������
            Debug.Log("shoulder vibrate");
            HapticFeedback();
        }


        // ����豸�Ƿ���Ч
        if (headDevice.isValid)
        {
            // ��ȡͷ�Ե�λ�ú���ת
            if (headDevice.TryGetFeatureValue(CommonUsages.devicePosition, out headsetPosition) && Vector3.Distance(headsetPosition, MouthPoint.position) < 0.2)
            {
                // ������
                Debug.Log("breath vibrate");
                HapticFeedback();
            }
        }
    }
    void HapticFeedback()
    {
        // ����һ�μ򵥵���
        RightController.SendHapticImpulse(0, 1.0f, 0.1f);
        LeftController.SendHapticImpulse(0, 1.0f, 0.1f);
    }
}
