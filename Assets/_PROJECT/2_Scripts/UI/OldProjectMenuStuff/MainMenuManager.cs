using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private int _firstSceneIndex = 1;

    private float _loadDuration = 1.5f;

    [SerializeField]
    private GameObject _mainMenu;

    [SerializeField]
    private GameObject _settingsMenu;

    [SerializeField]
    private GameObject _loadingScreen;

    private void Awake()
    {
        _mainMenu.SetActive(true);
        _settingsMenu.SetActive(false);
        _loadingScreen.SetActive(false);
    }

    public void OnNewGameStart()
    {
        StartCoroutine(LoadAsync(_firstSceneIndex));
    }

    public void OnSettings()
    {
        _settingsMenu.SetActive(true);
        _mainMenu.SetActive(false);
    }

    public void OnQuit()
    {
        Application.Quit();
        Debug.Log("quitting game");
    }

    public void CloseMenus()
    {
        if (_settingsMenu.activeInHierarchy)
        {
            _settingsMenu.SetActive(false);

            _mainMenu.SetActive(true);
            // For closing menus within the main menu
        }
    }

    IEnumerator LoadAsync(int sceneID)
    {
        // Load by scene index

        _loadingScreen.SetActive(true);

        yield return new WaitForSeconds(_loadDuration);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

    }
}
