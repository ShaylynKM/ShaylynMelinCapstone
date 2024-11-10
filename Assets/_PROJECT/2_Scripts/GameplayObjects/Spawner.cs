using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveStrategy))]
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

    [SerializeField] protected float _speed;

    [Tooltip("Amplitude of sine wave movement for spawned objects that use the SineWaveMovement class.")]
    [SerializeField] protected float _amplitude;

    [Tooltip("Frequency of sine wave movement for spawned objects that use the SineWaveMovement class.")]
    [SerializeField] protected float _frequency;

    [Tooltip("Whether or not the sine wave formation for an object using SineWaveMovement.cs is inverted.")]
    [SerializeField] private bool _invertSine = false;

    protected MoveStrategy _moveStrategy;
    protected Vector3 _spawnLocation;
    protected PoolObject _poolObject;

    public virtual void Start()
    {
        _spawnLocation = this.transform.position;
    }

    public virtual void SpawnObject()
    {
        PoolObject obj = PoolManager.Instance.Spawn(_poolObject); // Spawns the object with the pool manager

        obj.transform.position = this.transform.position; // Position is on the spawner

        Vector3 direction = new Vector3(Mathf.Cos(_spawnedObjectAngle * Mathf.Deg2Rad), Mathf.Sin(_spawnedObjectAngle * Mathf.Deg2Rad), 0);

        MoveStrategy objMoveStrategy = obj.GetComponent<MoveStrategy>();

        if (objMoveStrategy != null)
        {
            objMoveStrategy.Speed = _speed;
            objMoveStrategy.Initialize(this.transform.position, direction); // Set the angle of the spawned object

            if(objMoveStrategy is SineWaveMovement sineWaveMovement)
            {
                sineWaveMovement.Amplitude = _amplitude;
                sineWaveMovement.Frequency = _frequency;
                sineWaveMovement.Inverted = _invertSine;
            }
        }

        OnObjectSpawned(obj);
    }

    public virtual void OnObjectSpawned(PoolObject obj)
    {
        // For additional logic involving the specific instance being spawned
    }
}
