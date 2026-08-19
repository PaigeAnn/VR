using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
public class SimpleHapticFeedback : MonoBehaviour
{
    public HapticImpulsePlayer leftController;
    public HapticImpulsePlayer rightController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //controller = GetComponent<HapticImpulsePlayer>();
    }

    public void PlayHapticFeedback(float amplitude, float duration)
    {

        if (leftController != null)
        {
            leftController.SendHapticImpulse(amplitude, duration);
        }

        if (rightController != null)
        {
            rightController.SendHapticImpulse(amplitude, duration);
        }
    }
}
