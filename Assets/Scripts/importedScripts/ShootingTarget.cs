using System.Collections;
using UnityEngine;

public class ShootingTarget : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField] private float health = 100f;

    [Header("Explosion")]
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Transform explosionPoint;
    [SerializeField] private float explosionLifetime = 3f;

    [Header("Hit Flash")]
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private float flashDuration = 0.1f;

    private static readonly int BaseColor =
        Shader.PropertyToID("_BaseColor");

    private static readonly int ColorProperty =
        Shader.PropertyToID("_Color");

    private MaterialPropertyBlock propertyBlock;
    private Color originalColor;
    private int colorProperty;
    private Coroutine flashRoutine;
    private bool isDead;
    private SpawnedTargetLifecycle lifecycle;

    private void Awake()
    {
        if (targetRenderer == null)
            targetRenderer = GetComponentInChildren<Renderer>();

        Material material = targetRenderer.sharedMaterial;

        colorProperty = material.HasProperty(BaseColor)
            ? BaseColor
            : ColorProperty;

        originalColor = material.GetColor(colorProperty);
        propertyBlock = new MaterialPropertyBlock();
        lifecycle = GetComponent<SpawnedTargetLifecycle>();
    }

    public void TakeDamage(float amount)
    {
        if (isDead)
            return;

        health -= amount;

        if (health <= 0f)
        {
            Die();
            return;
        }

        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(Flash());
    }

    private void Die()
    {
        isDead = true;

        if (explosionPrefab != null)
        {
            Vector3 position = explosionPoint != null
                ? explosionPoint.position
                : transform.position;

            GameObject explosion = Instantiate(
                explosionPrefab,
                position,
                Quaternion.identity);

            Destroy(explosion, explosionLifetime);
        }

        if (lifecycle != null)
            lifecycle.RemoveAndReplace();
        else
            Destroy(gameObject);
    }

    private IEnumerator Flash()
    {
        SetColor(flashColor);

        yield return new WaitForSeconds(flashDuration);

        SetColor(originalColor);
        flashRoutine = null;
    }

    private void SetColor(Color color)
    {
        targetRenderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetColor(colorProperty, color);
        targetRenderer.SetPropertyBlock(propertyBlock);
    }
}