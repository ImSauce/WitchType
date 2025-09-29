using UnityEngine;

public class ParallaxMouse : MonoBehaviour
{
    [Header("Settings")]
    public float movementRange = 1f; // how far layers can move max
    public ParallaxLayer[] layers;   // assign layers manually in inspector

    void Update()
    {
        // Get mouse position normalized (0..1)
        Vector2 mousePos = Input.mousePosition;
        Vector2 normalized = new Vector2(
            Mathf.Clamp01(mousePos.x / Screen.width),
            Mathf.Clamp01(mousePos.y / Screen.height)
        );

        // Convert 0..1 into -1..1 range (center = 0)
        Vector2 centered = (normalized - new Vector2(0.5f, 0.5f)) * 2f;

        // Scale by movement range
        Vector2 offset = centered * movementRange;

        // Move each layer
        foreach (ParallaxLayer layer in layers)
        {
            if (layer != null)
                layer.Move(offset);
        }
    }
}