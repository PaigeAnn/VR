using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class XRHitscanGun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform muzzle;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioSource fireAudio;

    [Header("Firing")]
    [SerializeField] private float damage = 20f;
    [SerializeField] private float range = 100f;
    [SerializeField] private float shotsPerSecond = 5f;
    [SerializeField] private LayerMask hitLayers = ~0;

    private float nextFireTime;

    [Header("Optional Haptic Feedback")]
    [SerializeField] private XRFireHaptics fireHaptics;

    private void Awake()
    {
        // Automatically connects when the haptic feedback component script is on the gun.
        if (fireHaptics == null)
        {
            TryGetComponent(out fireHaptics);
        }
    }

    public void Fire()
    {
        if (Time.time < nextFireTime)
            return;

        nextFireTime = Time.time + 1f / shotsPerSecond;

        //Calls optional Haptic feeback
        fireHaptics?.Play();

        //Calls Visual feedback and Audio feedback
        muzzleFlash?.Play();
        fireAudio?.Play();

        Ray ray = new Ray(muzzle.position, muzzle.forward);

        if (Physics.Raycast(
                ray,
                out RaycastHit hit,
                range,
                hitLayers,
                QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.TryGetComponent(out IDamageable target))
                target.TakeDamage(damage);

            Debug.DrawLine(
                muzzle.position,
                hit.point,
                Color.red,
                1f);
        }
        else
        {
            Debug.DrawRay(
                muzzle.position,
                muzzle.forward * range,
                Color.red,
                1f);
        }
    }
}

public interface IDamageable
{
    void TakeDamage(float amount);
}