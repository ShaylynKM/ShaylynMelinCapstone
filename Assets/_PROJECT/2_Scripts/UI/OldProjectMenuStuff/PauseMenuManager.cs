using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : Singleton<PauseMenuManager>
{
    [SerializeField]
    private GameObject _pauseMenu;

    [SerializeField]
    private GameObject _quitMainMenu;

    [SerializeField]
    private GameObject _settingsMenu;

    public bool IsPaused { get; private set; }

    private void Start()
    {
        _isPersistent = true;

        _pauseMenu.SetActive(false);
        _quitMainMenu.SetActive(false);
        _settingsMenu.SetActive(false);

        IsPaused = false;
    }

    public void OnPause()
    {
        Time.timeScale = 0f;

        _pauseMenu.SetActive(true);

        IsPaused = true;
    }

    public void OnResume()
    {
        _pauseMenu.SetActive(false);

        Time.timeScale = 1f;

        IsPaused = false;
    }

    public void OnMainMenu()
    {
        _quitMainMenu.SetActive(true);
        _pauseMenu.SetActive(false);
    }

    public void OnQuitToMainMenu()
    {
        Time.timeScale = 1f; // Resume normal time to avoid unintended behaviour

        IsPaused = false;

        SceneManager.LoadScene("MainMenu");
    }

    public void OnSettings()
    {
        _settingsMenu.SetActive(true);
        _pauseMenu.SetActive(false);
    }

    public void CloseMenus()
    {
        if (_quitMainMenu.activeInHierarchy || _settingsMenu.activeInHierarchy)
        {
            _quitMainMenu.SetActive(false);
            _settingsMenu.SetActive(false);

            _pauseMenu.SetActive(true);

            // For closing menus within the main menu
        }
    }
}
