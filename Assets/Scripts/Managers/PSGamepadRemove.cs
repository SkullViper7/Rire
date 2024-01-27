using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PSGamepadRemove : MonoBehaviour
{
    private void OnEnable()
    {
        CheckAndRemovePS4Gamepad();
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (change == InputDeviceChange.Added && device.name == "DualShock4GamepadHID")
        {
            InputSystem.RemoveDevice(device);
        }
    }

    private void CheckAndRemovePS4Gamepad()
    {
        InputDevice ps4Gamepad = InputSystem.GetDevice("DualShock4GamepadHID");

        if (ps4Gamepad != null)
        {
            InputSystem.RemoveDevice(ps4Gamepad);
        }
    }
}