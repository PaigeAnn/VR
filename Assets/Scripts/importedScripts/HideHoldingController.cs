using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
public class HideHoldingController : MonoBehaviour
{
    [Header("Left Controller")]
    [SerializeField] private Transform leftControllerRoot;
    [SerializeField] private GameObject leftControllerVisual;

    [Header("Right Controller")]
    [SerializeField] private Transform rightControllerRoot;
    [SerializeField] private GameObject rightControllerVisual;

    private XRGrabInteractable grabInteractable;
    private GameObject hiddenVisual;

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

        ShowHiddenVisual();
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        Transform interactor = args.interactorObject.transform;

        if (IsPartOfController(interactor, leftControllerRoot))
            HideVisual(leftControllerVisual);
        else if (IsPartOfController(interactor, rightControllerRoot))
            HideVisual(rightControllerVisual);
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        ShowHiddenVisual();
    }

    private static bool IsPartOfController(
        Transform interactor,
        Transform controllerRoot)
    {
        return controllerRoot != null &&
               (interactor == controllerRoot ||
                interactor.IsChildOf(controllerRoot));
    }

    private void HideVisual(GameObject visual)
    {
        ShowHiddenVisual();

        hiddenVisual = visual;

        if (hiddenVisual != null)
            hiddenVisual.SetActive(false);
    }

    private void ShowHiddenVisual()
    {
        if (hiddenVisual != null)
            hiddenVisual.SetActive(true);

        hiddenVisual = null;
    }
}