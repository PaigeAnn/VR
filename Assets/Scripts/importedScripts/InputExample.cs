using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using System.Collections;

public class InputExample : MonoBehaviour
{
    [Header("Input Actions")]
    public InputActionReference xButton;
    public InputActionReference leftThumbStick;

    [Header("UI Output Displays")]
    public TMP_Text outputDisplay1;
    public TMP_Text outputDisplay2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool xButtonPress = xButton.action.IsPressed();
        if (xButtonPress)
        {
            //Button is pressed
            outputDisplay1.text = "Pressed the X Button";
            StartCoroutine(ClearDisplayField(outputDisplay1, 1f));

        }

        Vector2 thumbstickOutput = leftThumbStick.action.ReadValue<Vector2>();

        outputDisplay2.text = $"x: {thumbstickOutput.x} | y: {thumbstickOutput.y}";

    }

    IEnumerator ClearDisplayField(TMP_Text displayField, float delay)
    {
        yield return new WaitForSeconds(delay);
        displayField.text = ""; 
    }
}
