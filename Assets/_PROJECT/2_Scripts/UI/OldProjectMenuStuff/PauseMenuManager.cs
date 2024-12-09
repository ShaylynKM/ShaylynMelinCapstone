using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : Singleton<PauseMenuManager>
{
    [SerializeField]
    private GameObject _pauseMenu;

    [SerializeField]
    private GameObject _settingsMenu;

    [SerializeField]
    private GameObject _loadingScreen;

    private float _loadDuration = 1.5f;

    public bool IsPaused { get; private set; }

    private void Start()
    {
        _isPersistent = true;

        _pauseMenu.SetActive(false);
        _settingsMenu.SetActive(false);
        _loadingScreen.SetActive(false);

        IsPaused = false;
        Debug.Log(IsPaused);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && IsPaused == false)
        {
            Debug.Log("pause");
            OnPause();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && IsPaused == true)
        {
            Debug.Log("resume");
            OnResume();
        }
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

    public void OnQuitToMainMenu()
    {
        Time.timeScale = 1f; // Resume normal time to avoid unintended behaviour

        IsPaused = false;

        StartCoroutine(LoadAsync("0_MainMenu"));
    }

    IEnumerator LoadAsync(string sceneName)
    {
        // Load by scene index

        _loadingScreen.SetActive(true);

        yield return new WaitForSeconds(_loadDuration);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

    }

    public void OnSettings()
    {
        _settingsMenu.SetActive(true);
        _pauseMenu.SetActive(false);
    }

    public void CloseMenus()
    {
        if (_settingsMenu.activeInHierarchy)
        {
            _settingsMenu.SetActive(false);

            _pauseMenu.SetActive(true);

            // For closing menus within the pause menu
        }
    }
}
