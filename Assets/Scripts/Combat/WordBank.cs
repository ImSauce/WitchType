using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class WordBank : MonoBehaviour
{
    private List<string> originalWords = new List<string>();

    public List<string> workingWords = new List<string>();

    private void Awake()
    {
        TextAsset wordFile = Resources.Load<TextAsset>("words");
        if (wordFile != null)
        {
            originalWords = wordFile.text
                .Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries)
                .Select(w => w.Trim())
                .ToList();
        }

        workingWords.AddRange(originalWords);
        Shuffle(workingWords);
        ConverToLower(workingWords);
    }

    private void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int random = Random.Range(i, list.Count);
            string temporary = list[i];

            list[i] = list[random];
            list[random] = temporary;
        }
    }

    private void ConverToLower(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i] = list[i].ToLower();
        }
    }

    public string GetWord()
    {
        if (workingWords.Count == 0)
        {
            workingWords.AddRange(originalWords);
            Shuffle(workingWords);
            ConverToLower(workingWords);
        }

        string newWord = workingWords.Last();
        workingWords.Remove(newWord);

        return newWord;
    }
}
