//  [小鈎ハレ]  //
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject ButtonPause;
    [SerializeField] private Animator playerAnimator;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void ButtonClickPause()
    {

        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        ButtonPause.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        playerAnimator.enabled = true;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        ButtonPause.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        playerAnimator.enabled = false;
    }


    public void Home()
    {
        SceneManager.LoadScene("Home");
        Time.timeScale = 1f;
        GameIsPaused = false;
        playerAnimator.enabled = true;
    }
}