using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    public GameObject blackPlane;

    public float eyeWetnessTehee;
    [SerializeField] float maxEyeWetnessTehee;

    [SerializeField] float blinkedTime;
    public bool blinking;

    private void Start()
    {
        eyeWetnessTehee = maxEyeWetnessTehee;
        DryEyes();
    }

    //manual blinking
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            eyeWetnessTehee = 0f;
            Blink();
            CancelInvoke("DryEyes");
            CancelInvoke("UnBlink");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            
            Invoke("UnBlink", blinkedTime);
            Invoke("DryEyes", blinkedTime);
        }
    }

    //auto blinking
    void DryEyes()
    {
        Invoke("DryEyes", 0.1f);
        
        eyeWetnessTehee -= .9f;
        
        if (eyeWetnessTehee <= 0 )
        {
            Blink();
        }
    }
    
    void Blink()
    {
        blackPlane.SetActive(true);
        Invoke("UnBlink", blinkedTime);
        blinking = true;
    }

    void UnBlink()
    {
        blackPlane.SetActive(false);
        eyeWetnessTehee = maxEyeWetnessTehee;
        blinking = false;
    }
}
