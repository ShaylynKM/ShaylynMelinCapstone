using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestingCommands : Singleton<TestingCommands>
{
    protected override void Awake()
    {
        _isPersistent = true;
        base.Awake();
    }

    public string MainMenuScene, Scene1, Scene2, Scene3, Scene4, Scene5, Scene6;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            string currentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentScene); // Reload this scene
        }
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene(MainMenuScene);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(Scene1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(Scene2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene(Scene3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SceneManager.LoadScene(Scene4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SceneManager.LoadScene(Scene5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SceneManager.LoadScene(Scene5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SceneManager.LoadScene(Scene6);
        }
    }
}
