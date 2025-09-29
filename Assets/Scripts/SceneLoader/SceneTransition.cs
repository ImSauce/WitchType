using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator transition;

    [SerializeField] private PauseMenu pause;
    public float transitionTime = 1f;

    public void Play()
    {
        // If you want to go to a specific scene by name:
        StartCoroutine(Transition("CH1_L1_R1"));
    }

    public void GoHomeMenu()
    {
        pause.Home();
        // If you want to go to scene index 0:
        StartCoroutine(Transition("MAIN MENU"));
    }

    IEnumerator Transition(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator Transition(string sceneName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }
}