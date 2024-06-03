using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    // Create a damage popup
    public static DamagePopup Create(Vector3 position, float damage, bool isCriticalHit)
    {
        Transform damagePopupTransform = Instantiate(
            GameAssets.instance.damagePopupPrefab,
            position,
            Quaternion.identity
        );
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damage, isCriticalHit);

        return damagePopup;
    }

    static int sortingOrder;

    // Components
    TextMeshPro textMesh;
    Color textColor;

    // Timers
    const float DISAPPEAR_TIMER_MAX = 1f;
    float disappearTimer;

    // Movement
    Vector3 moveVector;
    float moveSpeed = 5f;

    // Color codes
    const string hitColorCode = "#FFA413";
    const string CriticalHitColorCode = "#FF0003";

    void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    void Update()
    {
        HandleMovement();
        HandleScaling();
        HandleDisappearance();
    }

    public void Setup(float damage, bool isCriticalHit)
    {
        textMesh.text = damage.ToString();

        // Set text size and color based on whether the hit is critical
        if (!isCriticalHit)
        {
            textMesh.fontSize = 5;
            if (ColorUtility.TryParseHtmlString(hitColorCode, out Color hitColor))
                textColor = hitColor;
        }
        else
        {
            textMesh.fontSize = 7;
            if (ColorUtility.TryParseHtmlString(CriticalHitColorCode, out Color criticalColor))
                textColor = criticalColor;
        }

        // Apply color and initialize timer
        textMesh.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;

        // Increase sorting order to make sure the popup appears on top
        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;

        moveVector = new Vector3(1, 1) * moveSpeed;
    }

    #region Handlers
    private void HandleMovement()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;
    }

    private void HandleScaling()
    {
        if (disappearTimer > DISAPPEAR_TIMER_MAX * 0.5f)
        {
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }
    }

    private void HandleDisappearance()
    {
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;

            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
    #endregion
}
