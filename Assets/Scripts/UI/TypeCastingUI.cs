//  [小鈎ハレ]  //
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TypeCastingUI : MonoBehaviour
{
    public static bool TypeCastingMode = false;

    // References 
    public PlayerMovement playermovement;
    public PauseMenu pausemenu;
    public Typer typer;
    public Transform firePoint;

    // UI
    public GameObject TypeCastUI;
    public GameObject spellContainer;
    public TMP_InputField TypeCastField;
    public Image spellIcon;
    
   

    private void Start()
    {
        typer.OnWordComplete += HandleWordComplete;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && SpellManager.Instance.selectedSpells != null)
        {
            if (!TypeCastingMode)
            {
                ActivateTypeCasting();
            }
            else
            {
                DeactivateTypeCasting();
            }
        }
    }

    public void ActivateTypeCasting()
    {
        playermovement.StopMovement();
        TypeCastUI.SetActive(true);
        playermovement.enabled = false;
        TypeCastingMode = true;
        pausemenu.enabled = false;

        var spell = SpellManager.Instance.selectedSpells.FirstOrDefault();
        if (spell != null && spell.spellPrefab != null)
        {
            spellContainer.SetActive(true);
            spellIcon.sprite = spell.spellIcon;
            spellIcon.enabled = true;
        }
        else
        {
            spellContainer.SetActive(false);
            spellIcon.enabled = false;
        }


        typer.ResetWord();

       
    }

    public void DeactivateTypeCasting()
    {
        TypeCastUI.SetActive(false);
        playermovement.enabled = true;
        TypeCastingMode = false;
        pausemenu.enabled = true;
        TypeCastField.text = string.Empty;
        EventSystem.current.SetSelectedGameObject(null);
    }
    
    private void HandleWordComplete()
    {
        DeactivateTypeCasting();
        CastSpell();
    }

    private void CastSpell()
    {
        var spell = SpellManager.Instance.selectedSpells.FirstOrDefault();
        if (spell != null && spell.spellPrefab != null)
        {
            Instantiate(spell.spellPrefab, firePoint.position, firePoint.rotation);
        }
      
    }
}

