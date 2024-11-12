using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(MoveStrategy))]
public class Spawner : MonoBehaviour
{
    [Tooltip("For homing bullets, set this as the player.")]
    [SerializeField] protected GameObject _homingTarget;

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
    [SerializeField] protected float _sineAmplitude;

    [Tooltip("Frequency of sine wave movement for spawned objects that use the SineWaveMovement class.")]
    [SerializeField] protected float _sineFrequency;

    [Tooltip("Whether or not the sine wave formation for an object using SineWaveMovement.cs is inverted.")]
    [SerializeField] protected bool _invertSine = false;

    [SerializeField] protected float _spiralAngleIncrement;
    [SerializeField] protected float _rateOfSpiralGrowth;

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

            if(objMoveStrategy is SineWaveMovement sineWaveMovement) // Assign the variables for sine waves
            {
                sineWaveMovement.Initialize(_spawnLocation, direction);

                sineWaveMovement.Amplitude = _sineAmplitude;
                sineWaveMovement.Frequency = _sineFrequency;
                sineWaveMovement.Inverted = _invertSine;
            }
            else if(objMoveStrategy is SpiralMovement spiralMovement) // Assign variables for spirals
            {
                spiralMovement.Initialize(_spawnLocation, direction);

                spiralMovement.AngleIncrement = _spiralAngleIncrement;
                spiralMovement.RateOfGrowth = _rateOfSpiralGrowth;
                spiralMovement.InitialPosition = _spawnLocation;
            }
            else if(objMoveStrategy is HomingMovement homingMovement) // Assign variables for homing objects
            {
                homingMovement.TargetToFollow = _homingTarget;
            }
        }
    }
}
