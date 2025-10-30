using UnityEngine;
using TMPro;

public class Typer : MonoBehaviour
{
    public WordBank wordBank;
    public TMP_Text wordOutput;
    public System.Action OnWordComplete;

    private string remainingWord = string.Empty;
    private string currentWord = string.Empty;
    private int correctIndex = 0;

    // Mobile
    private TouchScreenKeyboard keyboard;

    private void Start()
    {
        SetCurrentWord();
    }

    private void Update()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        CheckHardwareInput();
#elif UNITY_ANDROID || UNITY_IOS
        CheckTouchInput();
#endif
    }

    public void SetKeyboard(TouchScreenKeyboard newKeyboard)
    {
        keyboard = newKeyboard;
    }

    private void CheckHardwareInput()
    {
        if (Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;
            if (keysPressed.Length == 1)
                EnterLetter(keysPressed);
        }
    }

    private void CheckTouchInput()
    {
        if (keyboard != null && keyboard.active && !string.IsNullOrEmpty(keyboard.text))
        {
            string newText = keyboard.text;
            string lastChar = newText.Substring(newText.Length - 1);
            EnterLetter(lastChar);
            keyboard.text = "";
        }
    }

    private void SetCurrentWord()
    {
        currentWord = wordBank.GetWord();
        SetRemainingWord(currentWord);
    }

    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;

        string typedPart = $"<color=green>{currentWord.Substring(0, correctIndex)}</color>";
        string remainingPart = currentWord.Substring(correctIndex);

        wordOutput.text = typedPart + remainingPart;
    }

    private void EnterLetter(string typedLetter)
    {
        if (IsCorrectLetter(typedLetter))
        {
            correctIndex++;
            RemoveLetter();
            if (IsWordComplete())
            {
                correctIndex = 0;
                SetCurrentWord();
                DeactivateKeyboard();  // Deactivate the keyboard when word is complete
            }
            else
            {
                SetRemainingWord(remainingWord);
            }
        }
        else
        {
            wordOutput.text = $"<color=green>{currentWord.Substring(0, correctIndex)}</color>"
                + $"<color=red>{currentWord[correctIndex]}</color>" +
                currentWord.Substring(correctIndex + 1);
        }
    }

    private bool IsCorrectLetter(string letter)
    {
        return remainingWord.IndexOf(letter) == 0;
    }

    private void RemoveLetter()
    {
        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }

    private bool IsWordComplete()
    {
        bool complete = remainingWord.Length == 0;
        if (complete && OnWordComplete != null)
        {
            OnWordComplete.Invoke();
        }
        return complete;
    }

    private void DeactivateKeyboard()
    {
#if UNITY_ANDROID || UNITY_IOS
        if (keyboard != null)
        {
            keyboard.active = false; // Deactivate the keyboard
        }
#endif
    }

    public void ResetWord()
    {
        SetCurrentWord();
    }
}
