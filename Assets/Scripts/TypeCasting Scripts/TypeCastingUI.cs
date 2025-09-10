//  [小鈎ハレ]  //
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TypeCastingUI : MonoBehaviour
{
    public static bool TypeCastingMode = false;
    public PlayerMovement playermovent;
    public GameObject ButtonPause;
    public GameObject TypeCastUI;
    public PauseMenu pausemenu;
    public TMP_InputField TypeCastField;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (TypeCastingMode == false)
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
        playermovent.StopMovement();
        TypeCastUI.SetActive(true);
        playermovent.enabled = false;
        TypeCastingMode = true;
        pausemenu.enabled = false;

        EventSystem.current.SetSelectedGameObject(TypeCastField.gameObject);
        TypeCastField.ActivateInputField();
    }

    public void DeactivateTypeCasting()
    {
        TypeCastUI.SetActive(false);
        playermovent.enabled = true;
        TypeCastingMode = false;
        pausemenu.enabled = true;
        TypeCastField.text = string.Empty;
        EventSystem.current.SetSelectedGameObject(null);
    }
    


    


}

