
using UnityEngine;
using UnityEngine.UI;

public class ChangeSkin : MonoBehaviour
{
    [Header("Animator Overrides")]
    public AnimatorOverrideController HatlessSkin;
    public AnimatorOverrideController HatSkin;

    [Header("UI")]
    public Toggle hatToggle;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Make sure toggle event is linked
        if (hatToggle != null)
            hatToggle.onValueChanged.AddListener(OnToggleChanged);

        // Set initial skin based on toggle state
        OnToggleChanged(hatToggle != null && hatToggle.isOn);
    }

    public void OnToggleChanged(bool isOn)
    {
        // if toggle is OFF → HatSkin active
        // if toggle is ON → HatlessSkin active
        if (isOn)
            animator.runtimeAnimatorController = HatlessSkin;
        else
            animator.runtimeAnimatorController = HatSkin;
    }
}




// using UnityEngine;

// public class ChangeSkin : MonoBehaviour
// {
//     public AnimatorOverrideController HatlessSkin;
//     public AnimatorOverrideController HatSkin;

//     public void Hatless_Skin()
//     {
//         GetComponent<Animator>().runtimeAnimatorController = HatlessSkin as RuntimeAnimatorController;
//     }
    
//     public void Hat_Skin()
//     {
//         GetComponent<Animator>().runtimeAnimatorController = HatSkin as RuntimeAnimatorController;
//     }
// }
