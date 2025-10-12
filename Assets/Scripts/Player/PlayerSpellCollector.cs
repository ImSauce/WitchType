using UnityEngine;

public class PlayerSpellCollector : MonoBehaviour
{
    public int spellIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            var spellManager = SpellManager.Instance;
            if (spellManager == null)
            {
                return;
            }

            if (spellIndex >= 0 && spellIndex < spellManager.allSpells.Count)
            {
                var spellUnlock = spellManager.allSpells[spellIndex];
                spellManager.UnlockSpell(spellUnlock);
            }
            Destroy(gameObject);
        }
    }
}
