using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarMover : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveDistance = 1.0f; // Avatar�ƶ��ľ���
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // �����ײ�Ķ����Ƿ���Player���ֲ�
        if (other.CompareTag("LeftHand")) // ȷ�����Ѿ���������ȷ��Tag
        {
            // ��ǰ�ƶ�Avatar
            transform.position += transform.forward * moveDistance;
        }
    }
}
