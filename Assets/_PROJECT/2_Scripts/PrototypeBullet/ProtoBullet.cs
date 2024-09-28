using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class ProtoBullet : MonoBehaviour
{
    [SerializeField]
    private Vector2 _direction; // Which way this bullet travels

    [SerializeField]
    protected float _speed; // How fast this bullet travels

    [SerializeField]
    protected bool _isTrigger = false; // Whether the collider is a trigger or not

    private CircleCollider2D _collider; // This object's collider

    private float _destroyTime = .02f; // How long before destroying the object on contact

    [SerializeField]
    protected GameObject _target;

    protected virtual void Awake()
    {
        _collider = this.GetComponent<CircleCollider2D>();

        Init(transform.position, _target);

        SetTrigger(); // Whether we should use OnTriggerEnter2D or OnCollisionEnter2D
    }

    public virtual void Init(Vector3 position, GameObject target)
    {
        this.transform.position = position;
        this._direction = (target.transform.position - transform.position).normalized; // Move towards the target
    }

    protected virtual void DestroySelf()
    {
        StartCoroutine(WaitToDestroySelf(_destroyTime));
    }

    IEnumerator WaitToDestroySelf(float time)
    {
        yield return new WaitForSeconds(time); // Wait before destroying so other events in response to collision with the bullet can fire
        Destroy(this.gameObject);
    }

    public virtual void SetTrigger()
    {
        if(_isTrigger == true)
        {
            _collider.isTrigger = true;
        }
        else if(_isTrigger == false)
        {
            _collider.isTrigger = false;
        }
    }

    protected virtual void Update()
    {
        this.transform.position += new Vector3(this._direction.x * _speed, this._direction.y * _speed, 0f) * Time.deltaTime; // Move the bullet
    }
}
