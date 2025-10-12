using UnityEngine;
using UnityEngine.UI;

public class ToggleSpellBook : MonoBehaviour
{
    public int spellIndex;
    public Image selectedBorderImage;

    private void Start()
    {
        // Ensure the selected border is hidden when the game starts
        selectedBorderImage.enabled = false;
    }
    public void Toggle()
    {
        var spellManager = SpellManager.Instance;
        if (spellManager == null) return;

        if (spellIndex >= 0 && spellIndex < spellManager.allSpells.Count)
        {
            var spell = spellManager.allSpells[spellIndex];

            if (spellManager.unlockedSpells.Contains(spell))
            {
                selectedBorderImage.enabled = !selectedBorderImage.enabled;
                spellManager.ToggleSpellByIndex(spellIndex);
            }
        }
    }
}
