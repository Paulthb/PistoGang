using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class PlayerController
{
    private UduinoDevice controller = null;
    private int[] buttonLastFrameArray = new int[4];
    private bool[] buttonDown = new bool[4];
    private bool[] buttonUp = new bool[4];
    private bool resetNeeded = false;
    public PlayerController(UduinoDevice controller)
    {
        //INIT CONTROLLER
        SetUduinoDevice(controller);

        //INIT BUTTON
        UduinoManager.Instance.pinMode(controller, 2, PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(controller, 3, PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(controller, 4, PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(controller, 5, PinMode.Input_pullup);
        buttonLastFrameArray[0] = 1;
        buttonLastFrameArray[1] = 1;
        buttonLastFrameArray[2] = 1;
        buttonLastFrameArray[3] = 1;

        //INIT LED
        UduinoManager.Instance.pinMode(controller, 14, PinMode.PWM);
        UduinoManager.Instance.pinMode(controller, 15, PinMode.PWM);
        UduinoManager.Instance.pinMode(controller, 16, PinMode.PWM);
        UduinoManager.Instance.pinMode(controller, 17, PinMode.PWM);
        //UduinoManager.Instance.analogWrite(controller, 14, 255);
        //UduinoManager.Instance.analogWrite(controller, 15, 255);
        //UduinoManager.Instance.analogWrite(controller, 16, 255);
    }

    // Update is called once per frame
    public void Update()
    {
        if (!resetNeeded)
        {
            for (int i = 0; i < 4; i++)
            {
                int value = UduinoManager.Instance.digitalRead(controller, i + 2);
                if (value == 0 && buttonLastFrameArray[i] == 1)
                    buttonDown[i] = true;
                else if ( value == 1 && buttonLastFrameArray[i] == 0)
                    buttonUp[i] = true;
                else
                {
                    buttonDown[i] = false;
                    buttonUp[i] = false;
                }

                buttonLastFrameArray[i] = value;
            }
        }

        if (UduinoManager.Instance.digitalRead(controller, 2) == 1 &&
            UduinoManager.Instance.digitalRead(controller, 3) == 1 &&
            UduinoManager.Instance.digitalRead(controller, 4) == 1 &&
            UduinoManager.Instance.digitalRead(controller, 5) == 1)
        {
            resetNeeded = false;
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

    public bool GetButtonUp(int pin)
    {
        return buttonUp[pin];
    }

    public bool GetButton(int pin)
    {
        return buttonLastFrameArray[pin] == 0 ? true : false;
    }

    public void ResetInputs()
    {
        for (int i = 0; i < 4; i++)
        {
            buttonDown[i] = false;
            buttonUp[i] = false;
        }
        resetNeeded = true;
    }
}
