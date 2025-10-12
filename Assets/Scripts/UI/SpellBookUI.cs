using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SpellBookUI : MonoBehaviour
{
    public TMP_Text counterText;
    public GameObject spellBookPanel;
    [SerializeField] private GameObject[] spellSlots;
    private bool initialized = false;


    void Update()
    {
        if (!initialized) return;
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


    private IEnumerator Start()
    {
        spellBookPanel.SetActive(false);
        yield return new WaitUntil(() => SpellManager.Instance != null);
        yield return null;
        UpdateSpellSlot();
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
        var sm = SpellManager.Instance;
        if (sm == null)
            return;

        int validSelectedCount = 0;
        foreach (var spell in sm.selectedSpells)
        {
            if (sm.unlockedSpells.Contains(spell))
                validSelectedCount++;
        }

        counterText.text = $"SPELL BOOK [{validSelectedCount} / {sm.maxSpells}]";
    }


    public void UpdateSpellSlot()
    {
        var spellManager = SpellManager.Instance;

        for (int i = 0; i < spellSlots.Length; i++)
        {
            GameObject slot = spellSlots[i];
            Image iconImage = slot.transform.Find("SpellIcon")?.GetComponent<Image>();
            GameObject lockedOverlay = slot.transform.Find("LockedOverlay")?.gameObject;

            if (iconImage == null) Debug.LogError($"[Slot {i}] SpellIcon not found in {slot.name}");
            if (lockedOverlay == null) Debug.LogError($"[Slot {i}] LockedOverlay not found in {slot.name}");

            if (i < spellManager.allSpells.Count)
            {
                SpellManager.Spell spell = spellManager.allSpells[i];
                iconImage.sprite = spell.spellIcon;
                bool unlocked = spellManager.unlockedSpells.Contains(spell);

                iconImage.enabled = true;
                lockedOverlay.SetActive(!unlocked);

                Debug.Log($"[Slot {i}] Set icon for {spell.spellName}, unlocked={unlocked}");
            }
            else
            {
                iconImage.enabled = false;
                lockedOverlay.SetActive(true);
            }
        }
    }


    public void UnlockSpell(int index)
    {
        var sm = SpellManager.Instance;
        if(index >= 0 && index <sm.allSpells.Count)
        {
            SpellManager.Spell spell = sm.allSpells[index];
            if(!sm.selectedSpells.Contains(spell))
            {
                sm.selectedSpells.Add(spell);
                UpdateSpellSlot();
                UpdateCounter();
            }
        }
    }
}
