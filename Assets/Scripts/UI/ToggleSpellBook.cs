using UnityEngine;

public class ToggleSpellBook : MonoBehaviour
{

    public void Toggle(GameObject SelectedBorderImage)
    {

        SelectedBorderImage.SetActive(!SelectedBorderImage.activeSelf);
    }
}

