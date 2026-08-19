using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

using System.Collections;

public class InputsWithEvents : MonoBehaviour
{
    public InputActionReference xButton;
    public InputActionReference leftThumbStick;
    public TMP_Text outputDisplay1;
    public TMP_Text outputDisplay2;

    public SimpleHapticFeedback hapticFeedback;

    private void OnEnable()
    {
        xButton.action.performed += XbuttonPress;
        xButton.action.canceled += XbuttonPress;

        xButton.action.Enable();

        leftThumbStick.action.performed += ThumbstickOutput;
        leftThumbStick.action.canceled += ThumbstickOutput;

        leftThumbStick.action.Enable();
    }

    private void OnDisable()
    {
        xButton.action.performed -= XbuttonPress;
        xButton.action.canceled -= XbuttonPress;

        xButton.action.Disable();

        leftThumbStick.action.performed -= ThumbstickOutput;
        leftThumbStick.action.canceled -= ThumbstickOutput;

        leftThumbStick.action.Disable();
    }

    void XbuttonPress(InputAction.CallbackContext ctx)
    {
        outputDisplay1.text = "You pressed the X button";
        StartCoroutine(ClearDisplayField(outputDisplay1, 1f));
        hapticFeedback.PlayHapticFeedback(1f, 1.5f);
    }

    void ThumbstickOutput(InputAction.CallbackContext ctx)
    {
        Vector2 thumbstickValues = ctx.ReadValue<Vector2>();
        outputDisplay2.text = $"x: {thumbstickValues.x} | y: {thumbstickValues.y}";
    }

    IEnumerator ClearDisplayField(TMP_Text displayField, float delay)
    {
        yield return new WaitForSeconds(delay);
        displayField.text = "";
    }
}
