
using System.Collections.Generic;
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
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
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
            if (SpellBookUI.Instance != null && SpellBookUI.Instance.isActiveAndEnabled)
            {
                SpellBookUI.Instance.UpdateSpellSlot();
            }
        }
    }



    public void ToggleSpellByIndex(int index)
    {

        if (index >= 0 && index < allSpells.Count)
        {
            Spell spell = allSpells[index];


            if (!unlockedSpells.Contains(spell))
            {
                return;
            }

            if (selectedSpells.Contains(spell))
            {
                selectedSpells.Remove(spell);
            }
            else if (selectedSpells.Count < maxSpells)
            {
                selectedSpells.Add(spell);
            }
        }
    }

    [System.Serializable]
    public class Spell
    {
        public string spellName;
        public Sprite spellIcon;
        public GameObject spellPrefab;
    }

}
