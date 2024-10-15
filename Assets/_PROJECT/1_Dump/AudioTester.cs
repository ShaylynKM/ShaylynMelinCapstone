using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioTester : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            AudioManager.Instance.PlayAudio("Mystery");
        }
        if(Input.GetMouseButtonDown(1))
        {
            AudioManager.Instance.PlayAudio("Test2");
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            AudioManager.Instance.PlayAudio("Test1");
        }
        
        if(Input.GetKeyDown(KeyCode.P))
        {
            AudioManager.Instance.PauseAudio("jojo");
        }
        if(Input.GetKeyUp(KeyCode.P))
        {
            AudioManager.Instance.UnpauseAudio("jojo");
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Instance.StopAudio("Mystery");
        }
    }

    private void Start()
    {
        AudioManager.Instance.PlayAudio("jojo");
    }

}
