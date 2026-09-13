using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

public class InputHandler : MonoBehaviour
{
    public UnityEvent leftControllerPressed;
    public UnityEvent leftControllerReleased;
    private bool _leftPressedPreviousValue = false;

    public UnityEvent rightControllerPressed;
    public UnityEvent rightControllerReleased;
    private bool _rightPressedPreviousValue = false;

    private InputDevice? _leftController = null;
    private InputDevice? _rightController = null;

    void OnEnable()
    {
        List<InputDevice> allDevices = new List<InputDevice>();
        InputDevices.GetDevices(allDevices);
        foreach (InputDevice device in allDevices)
        {
            InputDevices_deviceConnected(device);
        }

        InputDevices.deviceConnected += InputDevices_deviceConnected;
        InputDevices.deviceDisconnected += InputDevices_deviceDisconnected;
    }

    private void OnDisable()
    {
        InputDevices.deviceConnected -= InputDevices_deviceConnected;
        InputDevices.deviceDisconnected -= InputDevices_deviceDisconnected;

        _leftController = null;
        _rightController = null;
    }

    private void InputDevices_deviceConnected(InputDevice device)
    {
        if ((device.characteristics & InputDeviceCharacteristics.Left) != 0)
        {
            _leftController = device;
        }
        else if ((device.characteristics & InputDeviceCharacteristics.Right) != 0)
        {
            _rightController = device;
        }
    }

    private void InputDevices_deviceDisconnected(InputDevice device)
    {
        if ((device.characteristics & InputDeviceCharacteristics.Left) != 0)
        {
            _leftController = null;
        }
        else if ((device.characteristics & InputDeviceCharacteristics.Right) != 0)
        {
            _rightController = null;
        }
    }

    private void Update()
    {
        if (_leftController.HasValue)
        {
            _leftController.Value.TryGetFeatureValue(CommonUsages.triggerButton, out bool leftPressed);
            if (leftPressed && !_leftPressedPreviousValue)
            {
                leftControllerPressed.Invoke();
            }
            else if (!leftPressed && _leftPressedPreviousValue)
            {
                leftControllerReleased.Invoke();
            }
            _leftPressedPreviousValue = leftPressed;
        }
        else if (_leftPressedPreviousValue)
        {
            leftControllerReleased.Invoke();
        }

        if (_rightController.HasValue)
        {
            _rightController.Value.TryGetFeatureValue(CommonUsages.triggerButton, out bool rightPressed);
            if (rightPressed && !_rightPressedPreviousValue)
            {
                rightControllerPressed.Invoke();
            }
            else if (!rightPressed && _rightPressedPreviousValue)
            {
                rightControllerReleased.Invoke();
            }
            _rightPressedPreviousValue = rightPressed;
        } else if (_rightPressedPreviousValue)
        {
            rightControllerReleased.Invoke();
        }
    }
}
