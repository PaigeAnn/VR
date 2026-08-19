using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[DisallowMultipleComponent]
[RequireComponent(typeof(XRGrabInteractable))]
public class XRFireHaptics : MonoBehaviour
{
    private enum HapticMode
    {
        SimpleImpulse,
        Curve
    }

    [Header("Mode")]
    [SerializeField] private HapticMode mode = HapticMode.Curve;

    [Header("Simple Impulse")]
    [Range(0f, 1f)]
    [SerializeField] private float simpleAmplitude = 0.55f;

    [Min(0.01f)]
    [SerializeField] private float simpleDuration = 0.08f;

    [Header("Curve Envelope")]
    [SerializeField]
    private AnimationCurve amplitudeCurve =
        new AnimationCurve(
            new Keyframe(0f, 1f),
            new Keyframe(0.15f, 0.65f),
            new Keyframe(0.4f, 0.2f),
            new Keyframe(1f, 0f));

    [Range(0f, 1f)]
    [SerializeField] private float maximumAmplitude = 0.65f;

    [Min(0.01f)]
    [SerializeField] private float envelopeDuration = 0.1f;

    [Range(0.01f, 0.03f)]
    [SerializeField] private float sampleInterval = 0.015f;

    private XRGrabInteractable grabInteractable;
    private XRBaseInputInteractor holdingInteractor;
    private Coroutine hapticRoutine;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        grabInteractable.selectExited.RemoveListener(OnReleased);

        StopHaptics();
        holdingInteractor = null;
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        holdingInteractor =
            args.interactorObject as XRBaseInputInteractor;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        if (!ReferenceEquals(
                args.interactorObject,
                holdingInteractor))
        {
            return;
        }

        StopHaptics();
        holdingInteractor = null;
    }

    /// <summary>
    /// Plays feedback on the controller currently holding this object.
    /// </summary>
    public void Play()
    {
        if (holdingInteractor == null)
            return;

        StopHaptics();

        if (mode == HapticMode.SimpleImpulse)
        {
            holdingInteractor.SendHapticImpulse(
                simpleAmplitude,
                simpleDuration);

            return;
        }

        hapticRoutine = StartCoroutine(
            PlayEnvelope(holdingInteractor));
    }

    public void StopHaptics()
    {
        if (hapticRoutine == null)
            return;

        StopCoroutine(hapticRoutine);
        hapticRoutine = null;
    }

    private IEnumerator PlayEnvelope(
        XRBaseInputInteractor targetInteractor)
    {
        float elapsed = 0f;

        while (elapsed < envelopeDuration)
        {
            if (!ReferenceEquals(
                    holdingInteractor,
                    targetInteractor))
            {
                break;
            }

            float normalizedTime =
                elapsed / envelopeDuration;

            float amplitude = Mathf.Clamp01(
                amplitudeCurve.Evaluate(normalizedTime) *
                maximumAmplitude);

            targetInteractor.SendHapticImpulse(
                amplitude,
                sampleInterval);

            yield return new WaitForSecondsRealtime(
                sampleInterval);

            elapsed += sampleInterval;
        }

        hapticRoutine = null;
    }
}