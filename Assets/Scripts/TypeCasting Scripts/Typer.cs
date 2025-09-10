
using UnityEngine;
using TMPro;

public class Typer : MonoBehaviour
{
    public WordBank wordBank;
    public TMP_InputField wordOutput;

    private string remainingWord = string.Empty;
    private string currentWord = string.Empty;

    private void Start()
    {
        SetCurrentWord();
        Debug.Log("Word set to: " + wordOutput.text);
    }

    private void SetCurrentWord()
    {
        currentWord = wordBank.GetWord();
        SetRemainingWord(currentWord);
    }

    private void SetRemainingWord(string newString)
    {
       remainingWord=newString;
        wordOutput.text = remainingWord;
    }   

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if(Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;
            if(keysPressed.Length == 1)
            {
                EnterLetter(keysPressed);
            }
        }
    }
    private void EnterLetter(string typedLetter)
    {
        if(IsCorrectLetter(typedLetter))
        {
            RemoveLetter();
            if(IsWordComplete())
            {
                SetCurrentWord();
            }
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
        return remainingWord.Length == 0;
    }
}
