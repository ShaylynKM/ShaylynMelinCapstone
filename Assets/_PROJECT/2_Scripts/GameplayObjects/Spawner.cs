using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveStrategy))]
[RequireComponent(typeof(Interval))]
public class Spawner : MonoBehaviour
{
    [SerializeField] protected GameObject _spawnObject;

    [Tooltip("This is how many objects should spawn in one go.")]
    [SerializeField] protected int _spawnAmount = 1;

    [Tooltip("Use this if we have multiple objects spawning at once.")]
    [SerializeField] protected List<float> _spawnedObjectAngles = new List<float>();

    [Tooltip("Use this is we only have one object spawning at once.")]
    [SerializeField] protected float _spawnedObjectAngle;

    [SerializeField] protected int _poolSize;

    protected MoveStrategy _moveStrategy;
    protected Interval _interval;
    protected Vector3 _spawnLocation;
    protected PoolObject _poolObject;

    public virtual void Start()
    {
        _spawnLocation = this.transform.position;
    }

    public virtual void SpawnObject()
    {
        
    }
}
