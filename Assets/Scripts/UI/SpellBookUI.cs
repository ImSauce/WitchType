using UnityEngine;
using TMPro;

public class SpellBookUI : MonoBehaviour
{
    public TMP_Text counterText;
    public GameObject spellBookPanel; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!spellBookPanel.activeSelf)
            {
                OpenSpellBook();
            }
            else
            {
                CloseSpellBook();
            }
        }
    }


    private void Start()
    {
        spellBookPanel.SetActive(false);
        UpdateCounter();
    }

    public void OpenSpellBook()
    {
        spellBookPanel.SetActive(true);
        UpdateCounter();
    }

    public void CloseSpellBook()
    {
        spellBookPanel.SetActive(false);
    }

    public void UpdateCounter()
    {
        counterText.text = "SPELL BOOK" + " " +  "[" + SpellManager.Instance.selectedSpells.Count + " / " + SpellManager.Instance.maxSpells + "]" ;
    }
}
