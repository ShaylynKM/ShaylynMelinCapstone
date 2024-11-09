using UnityEngine;

[RequireComponent(typeof(MoveStrategy))]
public class Spawner : MonoBehaviour
{
    [SerializeField] protected GameObject _spawnObject;

    [Tooltip("This is how many objects should spawn in one go.")]
    [SerializeField] protected int _spawnAmount;

    [SerializeField] protected int _poolSize;
    [SerializeField] protected Vector2 _objectDirection;

    protected MoveStrategy _moveStrategy;
    protected Vector3 _spawnLocation;
    protected PoolObject _poolObject;

    public virtual void Start()
    {
        _spawnLocation = this.transform.position;
    }

    public virtual void SpawnObject(GameObject prefab, Vector3 location)
    {

    }
}
