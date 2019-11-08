using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
public class PlayerController
{

    public int portNum;
    SerialPort Serial;
    string[] val = new string[5];
    private int[] buttonLastFrameArray = new int[4];
    private bool[] buttonDown = new bool[4];
    private bool[] buttonUp = new bool[4];
    private bool resetNeeded = false;
    private float[] coolDown = new float[4];
    private float coolDownDuration = 0.3f;
    public PlayerController(char portNum)
    {
        //INIT CONTROLLER
        //SetUduinoDevice(controller);
        //Debug.Log("COM" + portNum);
        Serial = new SerialPort("COM" + portNum, 57600);
        Serial.ReadTimeout = 50;
        //INIT BUTTON
        /*
        UduinoManager.Instance.pinMode(controller, 2, PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(controller, 3, PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(controller, 4, PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(controller, 5, PinMode.Input_pullup);
        UduinoManager.Instance.digitalWrite(controller, 2, 1);
        UduinoManager.Instance.digitalWrite(controller, 3, 1);
        UduinoManager.Instance.digitalWrite(controller, 4, 1);
        UduinoManager.Instance.digitalWrite(controller, 5, 1);
        */
        for (int i = 0; i < buttonLastFrameArray.Length; i++)
        {
            buttonLastFrameArray[i] = 1;
            buttonDown[i] = false;
            buttonUp[i] = false;
        }

        //INIT LED
        /*
         * 
        UduinoManager.Instance.pinMode(controller, 9, PinMode.Output);
        UduinoManager.Instance.pinMode(controller, 10, PinMode.Output);
        UduinoManager.Instance.pinMode(controller, 11, PinMode.Output);
        */
        /*
        UduinoManager.Instance.digitalWrite(controller, 9, State.HIGH);
        UduinoManager.Instance.digitalWrite(controller, 10, State.HIGH);
        UduinoManager.Instance.digitalWrite(controller, 11, State.HIGH);
        */
    }

    // Update is called once per frame
    public void Update(float deltatime)
    {
        if (Serial.IsOpen)
        {
            val = Serial.ReadLine().Split(',');
            //Debug.Log(val.Length);
            if (!resetNeeded && val.Length > 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    //Debug.Log(i+1+"/"+val.Length);
                    //int value = UduinoManager.Instance.digitalRead(controller, i + 2);
                    int value = int.Parse(val[i + 1]);
                    if (value == 0 && buttonLastFrameArray[i] == 1)
                    {
                        buttonDown[i] = true;
                        coolDown[i] = coolDownDuration;
                    }
                    else if (value == 1 && buttonLastFrameArray[i] == 0)
                    {
                        buttonUp[i] = true;
                        coolDown[i] = coolDownDuration;
                    }
                    else
                    {
                        if (buttonDown[i] && coolDown[i] <= 0)
                            buttonDown[i] = false;
                        else
                            coolDown[i] -= deltatime;

                        if (buttonUp[i] && coolDown[i] <= 0)
                            buttonUp[i] = false;
                        else
                            coolDown[i] -= deltatime;
                    }

                    buttonLastFrameArray[i] = value;
                }
            }
            if(val.Length > 1)
            {
                if (int.Parse(val[1]) == 1 &&
                int.Parse(val[2]) == 1 &&
                int.Parse(val[3]) == 1 &&
                int.Parse(val[4]) == 1)
                {
                    resetNeeded = false;
                }
            }
        }
        else
        {
            Serial.Open();
            SetLed(0);
        }
    }
    /*
    public void SetUduinoDevice(UduinoDevice controller)
    {
        this.controller = controller;
    }*/

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

    public void LedOn(int pin)
    {
        //UduinoManager.Instance.digitalWrite(controller, pin, State.HIGH);
    }

    public void LedOff(int pin)
    {
        //UduinoManager.Instance.digitalWrite(controller, pin, State.LOW);
    }
    public void SetLed(int ledId)
    {
        Serial.Write(ledId.ToString());

    }
    public void CloseSerial()
    {
        Serial.Close();
    }

    IEnumerator resetLed()
    {
        SetLed(0);
        yield return new WaitForSeconds(1f);
    }
    /*
    public void SendCommand()
    {
        UduinoManager.Instance.sendCommand("GetVariable");
    }
    */
    /*
    public UduinoDevice GetUduinoDevice()
    {
        return controller;
    }
    */
}
