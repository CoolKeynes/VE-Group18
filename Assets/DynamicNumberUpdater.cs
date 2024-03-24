using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DynamicNumberUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UpdateDisplay(); 
    }

    // Update is called once per frame
    void Update()
    {
        {
            // 每秒更新数字
            if (Time.frameCount % 60 == 0) 
            {
                number++; 
                UpdateDisplay();
            }
        }
    }
    private int number = 0; 


    void UpdateDisplay()
    {
        //numberText.text = number.ToString(); 
    }
}