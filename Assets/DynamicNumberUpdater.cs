using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DynamicNumberUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UpdateDisplay(); // ��ʼʱ����һ����ʾ
    }

    // Update is called once per frame
    void Update()
    {
        {
            // ÿ���������
            if (Time.frameCount % 60 == 0) // ����һ���򵥵����ӣ�ʵ��Ӧ�����������Ҫһ�������ӵĸ����߼�
            {
                number++; // �ı�����
                UpdateDisplay();
            }
        }
    }
    //public Text numberText; // ����UI Text���
    private int number = 0; // Ҫ��ʾ������


    void UpdateDisplay()
    {
        //numberText.text = number.ToString(); // ������ת��Ϊ�ַ�������ʾ��Text�����
    }
}