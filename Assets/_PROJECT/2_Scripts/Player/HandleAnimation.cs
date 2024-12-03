using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleAnimation : MonoBehaviour
{
    [SerializeField] Sprite _defaultSprite;

    private Animator _animator;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        Debug.Log("got the components");
        _animator.enabled = false;
        _renderer.sprite = _defaultSprite;
        Debug.Log("wow");
    }

    public void StartAnimation()
    {
        Debug.Log("Start animation");
        _animator.enabled = true;
        Debug.Log("Finished start");
    }

    public void StopAnimation()
    {
        Debug.Log("Stop animation");
        _animator.enabled = false;
        _renderer.sprite = _defaultSprite;
        Debug.Log("Finished stop");
    }
}
