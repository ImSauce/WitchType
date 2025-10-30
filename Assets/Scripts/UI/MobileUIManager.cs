using UnityEngine;

public class MobileUIManager : MonoBehaviour
{
    public RectTransform uiContainer; // Assign your Mobile UI panel here
    public float bottomOffset = 20f;
    private float lastKeyboardHeight = 0f;

    void Update()
    {
#if UNITY_ANDROID || UNITY_IOS
        float keyboardHeight = GetKeyboardHeight();

        if (Mathf.Abs(keyboardHeight - lastKeyboardHeight) > 1f)
        {
            lastKeyboardHeight = keyboardHeight;
            AdjustUI(keyboardHeight);
        }
#endif
    }

    void AdjustUI(float keyboardHeight)
    {
        Vector2 anchoredPos = uiContainer.anchoredPosition;
        anchoredPos.y = keyboardHeight > 0 ? keyboardHeight / 2f + bottomOffset : bottomOffset;
        uiContainer.anchoredPosition = anchoredPos;
    }

    float GetKeyboardHeight()
    {
        if (TouchScreenKeyboard.visible)
        {
            // Convert height from pixels to canvas space
            return TouchScreenKeyboard.area.height / Screen.dpi * 160f;
        }
        return 0f;
    }
}
