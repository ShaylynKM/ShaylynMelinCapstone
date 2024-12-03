using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private int _firstSceneIndex = 1;

    [SerializeField]
    private Canvas _mainMenuCanvas;

    [SerializeField]
    private GameObject _settingsMenu;

    [SerializeField]
    private GameObject _newGameMenu;

    [SerializeField]
    private GameObject _quitGameMenu;

    [SerializeField]
    private Button _continueButton;

    [SerializeField]
    private GameObject _warningText;

    [SerializeField]
    private GameObject _loadingScreen;

    private string _savedScene;

    private void Awake()
    {
        _settingsMenu.SetActive(false);
        _newGameMenu.SetActive(false);
        _quitGameMenu.SetActive(false);
        _loadingScreen.SetActive(false);

        //// Checking to see if there's save data
        //SaveData data = SaveManager.OnLoadGame();
        //if (data != null)
        //{
        //    _savedScene = data.SavedScene;
        //    _continueButton.interactable = true; // If save data exists, enable the continue button
        //}
        //else
        //{
        //    _continueButton.interactable = false; // If there is no saved data, disable the continue button
        //}
    }

    public void OnNewGameButton()
    {
        _newGameMenu.SetActive(true);

        if(_savedScene != null)
        {
            _warningText.SetActive(true);
        }
    }

    public void OnNewGameStart()
    {
        //SaveManager.ClearSave();
        StartCoroutine(LoadSceneAsync(_firstSceneIndex));
    }

    public void OnContinue()
    {
        if (_savedScene != null)
        {
            Debug.Log("loading scene" + _savedScene);

            StartCoroutine(LoadSceneAsync(_savedScene));
        }
    }

    public void OnSettings()
    {
        _settingsMenu.SetActive(true);
    }

    public void OnQuitButton()
    {
        _quitGameMenu.SetActive(true);
    }

    public void OnQuit()
    {
        Application.Quit();
        Debug.Log("quitting game");
    }

    public void CloseMenus()
    {
        if (_settingsMenu.activeInHierarchy || _quitGameMenu.activeInHierarchy || _newGameMenu.activeInHierarchy)
        {
            _settingsMenu.SetActive(false);
            _quitGameMenu.SetActive(false);
            _newGameMenu.SetActive(false);

            // For closing menus within the main menu
        }
    }

    // For asynchronous loading
    IEnumerator LoadSceneAsync(string sceneName)
    {
        // Load by scene name

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        _loadingScreen.SetActive(true);

        yield return null;
    }

    IEnumerator LoadSceneAsync(int sceneID)
    {
        // Load by scene index

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        _loadingScreen.SetActive(true);

        Debug.Log("Async load");

        yield return null;
    }
}
