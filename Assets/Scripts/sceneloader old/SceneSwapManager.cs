using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwapManager : MonoBehaviour
{
    public static SceneSwapManager instance;
    private SceneLoadInteraction.DoorToSpawnAt _doortoSpawnto;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static void SwapSceneFromDoorUse(SceneField myScene, SceneLoadInteraction.DoorToSpawnAt doorToSpawnAt)
    {
        instance.StartCoroutine(instance.FadeOutThenChangeScene(myScene, doorToSpawnAt));
    }

    private IEnumerator FadeOutThenChangeScene(SceneField myScene, SceneLoadInteraction.DoorToSpawnAt doorToSpawnAt = SceneLoadInteraction.DoorToSpawnAt.None)
    {
        //start fade to black
        SceneFadeManager.instance.StartFadeOut();
        //keep fading out
        while (SceneFadeManager.instance.IsFadingOut)
        {
            yield return null;
        }

        
        _doortoSpawnto = doorToSpawnAt;
        SceneManager.LoadScene(myScene);
    }
}

