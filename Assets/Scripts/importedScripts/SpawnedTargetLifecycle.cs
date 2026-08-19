using UnityEngine;

public class SpawnedTargetLifecycle : MonoBehaviour
{
    [SerializeField] private bool ignoreGroundPlane = true;

    private TargetSpawner spawner;
    private float removalHeight;
    private bool removalRequested;
    private bool initialized;

    public void Initialize(
        TargetSpawner owningSpawner,
        Collider groundCollider,
        float removeBelowHeight)
    {
        spawner = owningSpawner;
        removalHeight = removeBelowHeight;
        initialized = true;

        if (ignoreGroundPlane && groundCollider != null)
            IgnoreGroundCollisions(groundCollider);
    }

    private void Update()
    {
        if (!initialized || removalRequested)
            return;

        if (transform.position.y < removalHeight)
            RemoveAndReplace();
    }

    public void RemoveAndReplace()
    {
        // Prevent a shot and the height check from both spawning replacements.
        if (removalRequested)
            return;

        removalRequested = true;

        if (spawner != null)
            spawner.SpawnTarget();

        Destroy(gameObject);
    }

    private void IgnoreGroundCollisions(Collider groundCollider)
    {
        Collider[] targetColliders =
            GetComponentsInChildren<Collider>();

        foreach (Collider targetCollider in targetColliders)
        {
            if (targetCollider != groundCollider)
            {
                Physics.IgnoreCollision(
                    targetCollider,
                    groundCollider,
                    true);
            }
        }
    }
}