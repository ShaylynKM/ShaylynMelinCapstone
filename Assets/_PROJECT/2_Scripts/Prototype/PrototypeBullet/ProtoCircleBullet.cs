using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoCircleBullet : ProtoBullet
{
    float _timeBeforeLaunch = 1f; // How long before the bullets move after spawning
    float _timeBeforeDestroy = .1f; // How long before the bullets are destroyed once they reach the target

    bool _hasLaunched = false; // If the bullet has already been launched
    bool _currentlyLaunching = false; // If the bullet is in motion or about to move

    protected override void Awake()
    {
        this._speed = 5f;
        this._isTrigger = true;

        base.Awake();
    }

    IEnumerator WaitToLaunch()
    {
        yield return new WaitForSeconds(_timeBeforeLaunch);
        _hasLaunched = true;
    }

    protected override void Update()
    {
        float targetDistance = Vector2.Distance(this.transform.position, _target.transform.position); // Distance between the bullet and its target
        float stopThreshold = 0.1f; // How close the bullet needs to be to its target to stop moving

        if(_hasLaunched == false && _currentlyLaunching == false)
        {
            _currentlyLaunching = true; // The bullet is about to move
            StartCoroutine(WaitToLaunch()); // Only start this coroutine once, when the bullets haven't launched yet
        }
        if(_hasLaunched == true) // Once the bullet has launched
        {
            if (targetDistance > stopThreshold)
            {
                this.transform.position += new Vector3(this._direction.x * _speed, this._direction.y * _speed, 0f) * Time.deltaTime; // Move the bullet
            }
            else
            {
                Invoke("DestroySelf", _timeBeforeDestroy); // Destroy the bullet once it's reached the stopping threshold
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerBattleController>() || (collision.gameObject.GetComponent<BulletDestroyer>()))
        {
            DestroySelf();
        }
    }
}
