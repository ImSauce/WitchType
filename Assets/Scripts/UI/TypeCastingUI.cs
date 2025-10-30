using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections; 
public class TypeCastingUI : MonoBehaviour
{
    public static bool TypeCastingMode = false;

    public PlayerMovement playermovement;
    public Typer typer;
    public Transform firePoint;

    public GameObject TypeCastUI;
    public GameObject spellContainer;
    public TMP_InputField TypeCastField;
    public Image spellIcon;


    public TMP_Text manaWarningText;
    private bool isManaWarningActive = false;

    private PauseMenu pausemenu;
    private SpellBookUI spellBookUI;
    

    [Header("SFX")]
    public AudioSource sfxSource;
    public AudioClip manawarningsound;
    private AudioManager audioManager;
    
    // Mobile
    private TouchScreenKeyboard keyboard;
   

    private void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        pausemenu = FindFirstObjectByType<PauseMenu>();
        spellBookUI = FindFirstObjectByType<SpellBookUI>();
        typer.OnWordComplete += HandleWordComplete;

    }
    void Update()
    {

#if UNITY_STANDALONE || UNITY_EDITOR
        if (TypeCastingMode && !TypeCastField.isFocused) return;
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
#endif
    }


    public void ActivateTypeCasting()
    {
        audioManager?.PlaySFX("Typecast");

        EventSystem.current.sendNavigationEvents = false;
        playermovement.StopMovement();
        TypeCastUI.SetActive(true);
        playermovement.enabled = false;
        TypeCastingMode = true;
        

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

        pausemenu.Resume();
        spellBookUI.CloseSpellBook();

        typer.ResetWord();

        StartCoroutine(FocusInputField());
    }

    public void DeactivateTypeCasting()
    {
        
        EventSystem.current.sendNavigationEvents = true;
        TypeCastUI.SetActive(false);
        playermovement.enabled = true;
        TypeCastingMode = false;
        TypeCastField.text = string.Empty;
        EventSystem.current.SetSelectedGameObject(null);
        pausemenu.Resume();
    }
    
    private void HandleWordComplete()
    {
        DeactivateTypeCasting();
        CastSpell();
    }

    private void CastSpell()
    {
        var spell = SpellManager.Instance.selectedSpells.FirstOrDefault();
        if (spell == null || spell.spellPrefab == null)
            return;

        if (PlayerMana.Instance != null && !PlayerMana.Instance.HasEnoughMana(spell.manaCost))
        {
            if (sfxSource != null && manawarningsound != null)
                sfxSource.PlayOneShot(manawarningsound);
            
            StartCoroutine(ShowManaWarning());
            return; 
        }

        PlayerMana.Instance.UseMana(spell.manaCost);
        Instantiate(spell.spellPrefab, firePoint.position, firePoint.rotation);
    }


    private IEnumerator ShowManaWarning()
    {
        if (isManaWarningActive) yield break;
        isManaWarningActive = true;

        manaWarningText.gameObject.SetActive(true);

       
        var anim = manaWarningText.GetComponent<Animator>();
        if (anim != null)
            anim.SetTrigger("Show");

        yield return new WaitForSeconds(2f);

        manaWarningText.gameObject.SetActive(false);
        isManaWarningActive = false;
    }

    private IEnumerator FocusInputField()
    {
        yield return new WaitForSeconds(0.1f);
        TypeCastField.Select();
        TypeCastField.ActivateInputField();
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        typer.SetKeyboard(keyboard);

    }

    public void OpenTypeCastBtn()
    {
        if (SpellManager.Instance != null)
        {
            ActivateTypeCasting();
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
            typer.SetKeyboard(keyboard);
        }
    }

}

