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

    public string Scene1, Scene2, Scene3;

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Alpha0))
        //{
        //    SceneManager.LoadScene(MainMenuScene);
        //}
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
    }
}
