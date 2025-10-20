using UnityEngine;
using System.Collections;

public class CollectPopUpUI : MonoBehaviour
{
    public static CollectPopUpUI instance;

    [Header("Popup Settings")]
    public GameObject collectPopupPrefab;
    public Transform popupParent;
    public float popupDuration = 1f;
    public string triggerTag = "Spell";

    private AudioManager audioManager;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        audioManager = UnityEngine.Object.FindFirstObjectByType<AudioManager>();
    }

    public void ShowCollectPopup()
    {
        if (collectPopupPrefab == null || popupParent == null)
        {
            Debug.LogWarning("CollectPopUpUI: Missing prefab or popupParent reference!");
            return;
        }
        audioManager.PlaySFX("BookCollect");

        GameObject popupInstance = Instantiate(collectPopupPrefab, popupParent);
        Animator popupAnimator = popupInstance.GetComponent<Animator>();

        

        StartCoroutine(HidePopupAfterDelay(popupInstance));
    }

    private IEnumerator HidePopupAfterDelay(GameObject popup)
    {
        yield return new WaitForSeconds(popupDuration);
        Destroy(popup);
    }
}





//  public void AddCollectible()
//     {
//         if (isGameOver) return;  // ignore if already ended
//         count++;
//         SetCountText();
//         // create a new popup each time
//         GameObject popupInstance = Instantiate(collectpopup, popupParent.transform);
//         audioManager.PlaySFX(audioManager.collect);

//         StartCoroutine(HidePopupAfterDelay(popupInstance));
//     }

//     private IEnumerator HidePopupAfterDelay(GameObject popup)
//     {
//         yield return new WaitForSeconds(1f);
//         Destroy(popup);
//     }