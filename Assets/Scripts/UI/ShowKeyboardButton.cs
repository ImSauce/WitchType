using TMPro;
using UnityEngine;

public class SimpleMobileTyper : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text wordOutput;

    [Header("Word Settings")]
    public string targetWord = "unity";

    private string typedText = "";
    private TouchScreenKeyboard keyboard;

    private void Start()
    {
        ShowKeyboard();
        UpdateTextDisplay();
    }

    private void Update()
    {
#if UNITY_ANDROID || UNITY_IOS
        CheckTouchInput();
#elif UNITY_STANDALONE || UNITY_EDITOR
        CheckKeyboardInput();
#endif
    }

    // --- INPUT HANDLING ---

    private void CheckKeyboardInput()
    {
        if (Input.anyKeyDown)
        {
            string input = Input.inputString;
            if (input.Length == 1)
                EnterLetter(input);
        }
    }

    private void CheckTouchInput()
    {
        if (keyboard == null || !keyboard.active)
            return;

        if (!string.IsNullOrEmpty(keyboard.text))
        {
            string newText = keyboard.text;
            string lastChar = newText.Substring(newText.Length - 1);
            EnterLetter(lastChar);
            keyboard.text = "";
        }
    }

    // --- MAIN LOGIC ---

    public void ShowKeyboard()
    {
#if UNITY_ANDROID || UNITY_IOS
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
#else
        Debug.Log("Keyboard only works on mobile platforms.");
#endif
    }

    private void EnterLetter(string letter)
    {
        if (typedText.Length >= targetWord.Length)
            return;

        // Compare to target letter
        char correctChar = targetWord[typedText.Length];
        if (letter.Equals(correctChar.ToString(), System.StringComparison.OrdinalIgnoreCase))
        {
            typedText += correctChar;
        }

        UpdateTextDisplay();

        // Check if word is complete
        if (typedText.Length == targetWord.Length)
        {
            DeactivateKeyboard();  // Deactivate the keyboard when word is complete
        }
    }

    private void UpdateTextDisplay()
    {
        if (wordOutput == null) return;

        string display = "";
        for (int i = 0; i < targetWord.Length; i++)
        {
            if (i < typedText.Length)
                display += $"<color=green>{targetWord[i]}</color>";
            else
                display += targetWord[i];
        }

        wordOutput.text = display;
    }

    // --- RESET ---

    public void ResetTyping()
    {
        typedText = "";
        UpdateTextDisplay();
    }

    // --- KEYBOARD DEACTIVATION ---
    private void DeactivateKeyboard()
    {
#if UNITY_ANDROID || UNITY_IOS
        if (keyboard != null)
        {
            keyboard.active = false; // Deactivate the keyboard
        }
#endif
    }
}
