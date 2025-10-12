
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public static SpellManager Instance;
    public List<Spell> selectedSpells = new List<Spell>();
    public List<Spell> unlockedSpells = new List<Spell>();
    public List<Spell> allSpells; 
    public int maxSpells = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Debug.LogWarning("Duplicate SpellManager found, destroying this instance!");
            Destroy(gameObject);
            return;
        }
        Debug.Log("SpellManager Awake in scene: " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

    }

    public void SelectSpell(Spell spell)
    {
        if (selectedSpells.Count < maxSpells && !selectedSpells.Contains(spell))
        {
            selectedSpells.Add(spell);
        }
    }

    public void DeselectSpell(Spell spell)
    {
        if (selectedSpells.Contains(spell))
        {
            selectedSpells.Remove(spell);
        }
    }

    public void UnlockSpell(Spell spell)
    {
        if (!unlockedSpells.Contains(spell))
        {
            unlockedSpells.Add(spell);

            var spellBookUI = FindFirstObjectByType<SpellBookUI>();
            if (spellBookUI != null)
            {
                spellBookUI.UpdateSpellSlot();
                spellBookUI.UpdateCounter();
            }
        }
    }


    public void ToggleSpellByIndex(int index)
    {

        if (index >= 0 && index < allSpells.Count)
        {
            Spell spell = allSpells[index];
           

            if (selectedSpells.Contains(spell))
            {
                selectedSpells.Remove(spell);
            }
            else if (selectedSpells.Count < maxSpells)
            {
                selectedSpells.Add(spell);
            }
        }

        FindFirstObjectByType<SpellBookUI>()?.UpdateCounter();
    }

    [System.Serializable]
    public class Spell
    {
        public string spellName;
        public Sprite spellIcon;
        public GameObject spellPrefab;
    }

}
