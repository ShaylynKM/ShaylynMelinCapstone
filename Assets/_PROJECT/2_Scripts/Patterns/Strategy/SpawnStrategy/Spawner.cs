using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(MoveStrategy))]
public class Spawner : MonoBehaviour
{
    [Header("GENERAL")]

    [SerializeField] protected int _poolSize;

    [SerializeField] protected float _spawnedObjectSpeed;

    [SerializeField] protected GameObject _spawnedObject;

    [SerializeField] protected bool _spawnerShouldRotate = false;

    [SerializeField] protected bool _spawnerShouldFollowPlayer = false;

    [SerializeField] protected float _spawnerRotationSpeed = 5;

    [SerializeField] protected GameObject _playerObject;

    [Tooltip("How long before an object starts moving.")]
    [SerializeField] private float _timeBeforeMoving;

    [Tooltip("How long before an object is despawned.")]
    [SerializeField] private float _timeBeforeDespawn;

    [Tooltip("This is how many objects should spawn in one go.")]
    [SerializeField] protected int _spawnAmount = 1;

    [Tooltip("Use this is we only have one object spawning at once.")]
    [SerializeField] protected float _spawnedObjectAngle;

    [Tooltip("Use this if we have multiple objects spawning at once.")]
    [SerializeField] protected List<float> _spawnedObjectAngles = new List<float>();


    [Header("SINE WAVE")]

    [Tooltip("Amplitude of sine wave movement for spawned objects that use the SineWaveMovement class.")]
    [SerializeField] protected float _sineAmplitude;

    [Tooltip("Frequency of sine wave movement for spawned objects that use the SineWaveMovement class.")]
    [SerializeField] protected float _sineFrequency;

    [Tooltip("Whether or not the sine wave formation for an object using SineWaveMovement.cs is inverted.")]
    [SerializeField] protected bool _invertSine = false;


    [Header("SPIRAL")]

    [Tooltip("Angle increment for objects using SpiralMovement.cs.")]
    [SerializeField] protected float _spiralAngleIncrement;

    [Tooltip("How fast the spiral grows for objects using SpiralMovement.cs.")]
    [SerializeField] protected float _rateOfSpiralGrowth;


    [Header("HOMING")]

    [Tooltip("For homing bullets, set this as the player.")]
    [SerializeField] protected GameObject _homingTarget;


    [Header("TRAP")]

    [Tooltip("The center of the 'trap' for objects that use TrapMovement.cs. ")]
    [SerializeField] private GameObject _trapTarget;


    protected MoveStrategy _moveStrategy;
    protected Vector3 _spawnLocation;
    protected PoolObject _poolObject;

    public virtual void Start()
    {
        _spawnLocation = this.transform.position;

        if(this._moveStrategy is MoveWithPlayer moveWithPlayer) // If this spawner is supposed to move with the player
        {
            transform.position = moveWithPlayer.PlayerObject.transform.position; // place this object on the player's transform
        }
    }

    public virtual void Update()
    {
        if(_spawnerShouldRotate == true)
        {
            transform.Rotate(0, 0, Time.deltaTime * _spawnerRotationSpeed);
        }

        if (this._moveStrategy is MoveWithPlayer moveWithPlayer) // If this spawner is supposed to move with the player
        {
            transform.position = _playerObject.transform.position; // Follow the player
        }
    }

    public virtual void SpawnObject()
    {
        bool hasSingleAngle = _spawnedObjectAngles.Count <= 1; // if the bullets should only travel in one direction, or if there are multiple angles

        // Iterate through the amount of projectiles spawned at once and give them their angles
        for (int i = 0; i < _spawnAmount; i++)
        {
            PoolObject obj = PoolManager.Instance.Spawn(_spawnedObject.GetComponent<PoolObject>()); // Spawns the object with the pool manager

            obj.transform.position = transform.position; // Position is on the spawner

            MoveStrategy objMoveStrategy = obj.GetComponent<MoveStrategy>();

            // Either use the list of angles if we have more than one angle, or default to _spawnedObjectAngle for singular ones
            float angle;

            if(hasSingleAngle)
            {
                angle = _spawnedObjectAngle; // If there's only one angle, use the default
            }
            else
            {
                angle = _spawnedObjectAngles[i % _spawnedObjectAngles.Count]; // If there are more than one angles, use the current angle from the list for this object. Wrap if we're at the end.
            }

            Vector3 localDirection = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0); // Local direction
            Vector3 worldDirection = transform.TransformDirection(localDirection); // World space direction

            if (objMoveStrategy != null)
            {
                objMoveStrategy.Speed = _spawnedObjectSpeed;
                objMoveStrategy.TimeBeforeDespawn = _timeBeforeDespawn;
                objMoveStrategy.TimeBeforeMoving = _timeBeforeMoving;

                if (objMoveStrategy is SineWaveMovement sineWaveMovement) // Assign the variables for sine waves
                {
                    sineWaveMovement.Initialize(transform.position, worldDirection);

                    sineWaveMovement.Amplitude = _sineAmplitude;
                    sineWaveMovement.Frequency = _sineFrequency;
                    sineWaveMovement.Inverted = _invertSine;
                }
                else if (objMoveStrategy is SpiralMovement spiralMovement) // Assign variables for spirals
                {
                    spiralMovement.Initialize(transform.position, worldDirection);

                    spiralMovement.AngleIncrement = _spiralAngleIncrement;
                    spiralMovement.RateOfGrowth = _rateOfSpiralGrowth;
                    spiralMovement.InitialPosition = transform.position;
                }
                else if (objMoveStrategy is HomingMovement homingMovement) // Assign variables for homing objects
                {
                    homingMovement.Initialize(transform.position, _homingTarget);

                    homingMovement.TargetToFollow = _homingTarget;
                }
                else if(objMoveStrategy is TrapMovement trapMovement) // Assign variables for traps
                {
                    trapMovement.Initialize(transform.position, _trapTarget);

                    trapMovement.Target = _trapTarget;

                    if (trapMovement.ReadyToDespawn == true)
                    {
                        Bullet bulletScript = obj.GetComponent<Bullet>();
                        bulletScript.OnDespawn();
                    }                      
                }
                else
                {
                    objMoveStrategy.Initialize(transform.position, worldDirection); // Default to this if there is no specific move strategy
                }
            }
        }
    }
}
