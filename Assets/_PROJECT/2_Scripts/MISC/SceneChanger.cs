using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Call with event
    private string _sceneName;

    private float _waitTime = 0.2f;

    public void ChangeScenes(string sceneName)
    {
        _sceneName = sceneName; // the scene name should be whatever I input in the inspector as a parameter in an event
        StartCoroutine(WaitThenChangeScenes(_sceneName));   
    }

    private IEnumerator WaitThenChangeScenes(string sceneName)
    {
        yield return new WaitForSeconds(_waitTime); // Wait briefly before changing the scene

        SceneManager.LoadScene(sceneName);

    }
}
