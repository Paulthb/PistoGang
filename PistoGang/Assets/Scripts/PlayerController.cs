using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class PlayerController
{
    UduinoDevice controller = null;
    int[] buttonLastFrameArray = new int[4];
    bool[] buttonDown = new bool[4];

    public PlayerController(UduinoDevice controller)
    {
        SetUduinoDevice(controller);
        UduinoManager.Instance.pinMode(controller, 2, PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(controller, 3, PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(controller, 4, PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(controller, 5, PinMode.Input_pullup);
        //INIT LED
        UduinoManager.Instance.pinMode(controller, 14, PinMode.PWM);
        UduinoManager.Instance.pinMode(controller, 15, PinMode.PWM);
        UduinoManager.Instance.pinMode(controller, 16, PinMode.PWM);
        UduinoManager.Instance.pinMode(controller, 17, PinMode.PWM);
        buttonLastFrameArray[0] = 1;
        buttonLastFrameArray[1] = 1;
        buttonLastFrameArray[2] = 1;
        buttonLastFrameArray[3] = 1;
        UduinoManager.Instance.analogWrite(controller, 14, 255);
        UduinoManager.Instance.analogWrite(controller, 15, 255);
        UduinoManager.Instance.analogWrite(controller, 16, 255);
        UduinoManager.Instance.analogWrite(controller, 17, 255);
    }

    // Update is called once per frame
    public void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            int value = UduinoManager.Instance.digitalRead(controller, i+2);
            if (value == 0 && buttonLastFrameArray[i] == 1)
            {
                buttonDown[i] = true;
            }
            else
                buttonDown[i] = false;
            buttonLastFrameArray[i] = value;
            if(buttonLastFrameArray[i] == 0)
                UduinoManager.Instance.analogWrite(controller, i + 14, 255);
            else
                UduinoManager.Instance.analogWrite(controller, i + 14, 0);
        }

    }

    public void SetUduinoDevice(UduinoDevice controller)
    {
        this.controller = controller;
    }

    public bool GetButtonDown(int pin)
    {
        return buttonDown[pin];
    }
}
