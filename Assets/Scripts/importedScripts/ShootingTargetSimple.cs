using System.Collections;
using UnityEngine;

public class ShootingTargetSimple : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField] private float health = 100f;

    [Header("Hit Flash")]
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private float flashDuration = 0.1f;

    private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
    private static readonly int ColorProperty = Shader.PropertyToID("_Color");

    private MaterialPropertyBlock propertyBlock;
    private Color originalColor;
    private int colorProperty;
    private Coroutine flashRoutine;

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
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(Flash());

        if (health <= 0f)
            Destroy(gameObject, flashDuration);
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