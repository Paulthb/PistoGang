using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;
using UnityEngine.UI;

public class myBtn : MonoBehaviour
{
    public Text textZone;

    // Start is called before the first frame update
    void Start()
    {
        UduinoManager.Instance.pinMode(2, PinMode.Input_pullup);
    }

    // Update is called once per frame
    void Update()
    {
        int buttonValueGreen = UduinoManager.Instance.digitalRead(2);
        int buttonValueRed = UduinoManager.Instance.digitalRead(4);

        if (buttonValueGreen == 0)
            textZone.text = "Down";
        else if (buttonValueRed == 0)
            textZone.text = "YOLO";
        else
            textZone.text = "Up";

    }
}
