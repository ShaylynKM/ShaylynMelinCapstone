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

        _animator.enabled = false;
        _renderer.sprite = _defaultSprite;
    }

    public void StartAnimation()
    {
        _animator.enabled = true;
    }

    public void StopAnimation()
    {
        _animator.enabled = false;
        _renderer.sprite = _defaultSprite;
    }
}
