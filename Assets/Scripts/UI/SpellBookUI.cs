using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SpellBookUI : MonoBehaviour
{
    public static SpellBookUI Instance;
    public TMP_Text counterText;
    public GameObject spellBookPanel;
    [SerializeField] private GameObject[] spellSlots;
    private bool initialized = false;

    private AudioManager audioManager;


    private void Awake()
    {
        Instance = this;
    }

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
        audioManager = UnityEngine.Object.FindFirstObjectByType<AudioManager>();

        spellBookPanel.SetActive(false);
        yield return new WaitUntil(() => SpellManager.Instance != null);

        // wait a few frames for TMP and UI Toolkit to finish loading
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        UpdateSpellSlot();
        UpdateCounter();
        initialized = true;
    }


    public void OpenSpellBook()
    {
        spellBookPanel.SetActive(true);
        UpdateCounter();
        audioManager.PlaySFX("Click");
    }

    public void CloseSpellBook()
    {
        spellBookPanel.SetActive(false);
        audioManager.PlaySFX("Click");
    }

    public void UpdateCounter()
    {
        var sm = SpellManager.Instance;
        if (sm == null)
            return;

        counterText.text = $"SPELL BOOK [{sm.selectedSpells.Count} / {sm.maxSpells}]";
    }


    public void UpdateSpellSlot()
    {
        var spellManager = SpellManager.Instance;

        for (int i = 0; i < spellSlots.Length; i++)
        {
            GameObject slot = spellSlots[i];
            Image iconImage = slot.transform.Find("SpellIcon")?.GetComponent<Image>();
            GameObject lockedOverlay = slot.transform.Find("LockedOverlay")?.gameObject;


            if (i < spellManager.allSpells.Count)
            {
                SpellManager.Spell spell = spellManager.allSpells[i];
                iconImage.sprite = spell.spellIcon;
                bool unlocked = spellManager.unlockedSpells.Contains(spell);

                iconImage.enabled = true;
                lockedOverlay.SetActive(!unlocked);
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
