using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform spawnCenter;
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private Collider groundCollider;

    [Header("Spawning")]
    [Min(1)]
    [SerializeField] private int startingTargetCount = 3;

    [Min(0f)]
    [SerializeField] private float spawnRadius = 5f;

    [Header("Launch")]
    [SerializeField] private float minimumLaunchSpeed = 6f;
    [SerializeField] private float maximumLaunchSpeed = 9f;

    [Header("Removal")]
    [Min(0f)]
    [SerializeField] private float removeDistanceBelowGround = 3f;

    private void Start()
    {
        for (int i = 0; i < startingTargetCount; i++)
            SpawnTarget();
    }

    public void SpawnTarget()
    {
        if (targetPrefab == null || spawnCenter == null)
        {
            Debug.LogError(
                "TargetSpawner requires a target prefab and spawn center.",
                this);

            return;
        }

        // Random point inside a horizontal circle around the Y-axis.
        Vector2 circlePoint = Random.insideUnitCircle * spawnRadius;

        Vector3 spawnPosition = spawnCenter.position +
            new Vector3(circlePoint.x, 0f, circlePoint.y);

        GameObject target = Instantiate(
            targetPrefab,
            spawnPosition,
            Random.rotation);

        SpawnedTargetLifecycle lifecycle =
            target.GetComponent<SpawnedTargetLifecycle>();

        if (lifecycle == null)
        {
            Debug.LogError(
                "The target prefab needs a SpawnedTargetLifecycle component.",
                target);

            Destroy(target);
            return;
        }

        float groundHeight = groundCollider != null
            ? groundCollider.transform.position.y
            : spawnCenter.position.y;

        lifecycle.Initialize(
            this,
            groundCollider,
            groundHeight - removeDistanceBelowGround);

        Rigidbody targetRigidbody = target.GetComponent<Rigidbody>();

        if (targetRigidbody != null)
        {
            float launchSpeed = Random.Range(
                minimumLaunchSpeed,
                maximumLaunchSpeed);

            targetRigidbody.AddForce(
                Vector3.up * launchSpeed,
                ForceMode.VelocityChange);
        }
        else
        {
            Debug.LogWarning(
                "The target prefab has no Rigidbody.",
                target);
        }
    }
}