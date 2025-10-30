using UnityEngine;

public class MobileAim : MonoBehaviour
{
    public static MobileAim Instance;

    [Tooltip("The draggable aim UI element.")]
    public RectTransform aimUI;
    [Tooltip("Canvas containing the aim UI.")]
    public Canvas canvas;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Returns the world position of the draggable aim UI.
    /// </summary>
    public Vector3 GetAimWorldPosition()
    {
        if (aimUI == null || canvas == null)
        {
            Debug.LogWarning("MobileAim: AimUI or Canvas not assigned!");
            return Vector3.zero;
        }

        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, aimUI.position);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0f));
        worldPos.z = 0f;
        return worldPos;
    }
}
