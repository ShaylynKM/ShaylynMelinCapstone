using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshake : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = false; // Keep the animation from playing on awake (I know there's a less scuffed way to do this but it's 2AM and I can't find it)
    }

    public void PlayScreenshake()
    {
        if(_animator.enabled == false)
        {
            _animator.enabled = true;
        }
        _animator.Play("Screenshake", 0,0); // Make sure the animation is set from the beginning so it can play more than once
    }  
}
