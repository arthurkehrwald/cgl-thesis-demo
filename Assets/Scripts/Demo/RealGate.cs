using UnityEngine;
using Uduino;

public class RealGate : MonoBehaviour
{
    [SerializeField]
    private VirtualGate virtualGate;
    [SerializeField]
    private int stepsPerCommand = 10;
    [SerializeField]
    private int stepsPerRevolution = 2048;

    private int stepsFromZero = 0;
    private bool followVirtualGate = false;
    private int openStepsFromZero;
    private void OnEnable()
    {
        UduinoManager.Instance.alwaysRead = true;
        UduinoManager.Instance.SetReadCallback(Arduino_OnValueReceived);
    }

    private void OnDisable()
    {
        UduinoManager.Instance.SetReadCallback(null);
    }

    private void Arduino_OnValueReceived(string data)
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            UduinoManager.Instance.sendCommand("turnCW");
            followVirtualGate = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            UduinoManager.Instance.sendCommand("turnCCW");
            followVirtualGate = false;
        }

        if (followVirtualGate)
        {
            stepsFromZero = (int)Mathf.Repeat(int.Parse(data), stepsPerRevolution);
            if (virtualGate.Openness > 0.1)
            {
                int a = 0;
            }
            int openSteps = Mathf.RoundToInt(-stepsPerRevolution / 4f);
            int targetStepsFromZero = Mathf.RoundToInt(openSteps * virtualGate.Openness);
            int clockwiseStepDelta = targetStepsFromZero - stepsFromZero;

            if (clockwiseStepDelta > stepsPerRevolution / 2)
                clockwiseStepDelta -= stepsPerRevolution;
            else if (clockwiseStepDelta < -stepsPerRevolution / 2)
                clockwiseStepDelta += stepsPerRevolution;

            if (clockwiseStepDelta < -stepsPerCommand)
                UduinoManager.Instance.sendCommand("turnCCW");
            else if (clockwiseStepDelta > stepsPerCommand)
                UduinoManager.Instance.sendCommand("turnCW");
        }

        if (Input.GetKey(KeyCode.Alpha0))
        {
            UduinoManager.Instance.sendCommand("setZero");
            followVirtualGate = true;
        }
    }
}
