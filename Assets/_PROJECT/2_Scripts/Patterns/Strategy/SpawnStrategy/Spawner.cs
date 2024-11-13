﻿using System.Collections.Generic;
using System.Threading;
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

    [SerializeField] protected float _spawnerRotationSpeed = 5;

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

    protected MoveStrategy _moveStrategy;
    protected Vector3 _spawnLocation;
    protected PoolObject _poolObject;

    public virtual void Start()
    {
        _spawnLocation = this.transform.position;
    }

    private void Update()
    {
        if(_spawnerShouldRotate == true)
        {
            transform.Rotate(0, 0, Time.deltaTime * _spawnerRotationSpeed);
        }
    }

    public virtual void SpawnObject()
    {
        bool hasSingleAngle = _spawnedObjectAngles.Count <= 1; // if the bullets should only travel in one direction, or if there are multiple angles

        // Iterate through the amount of projectiles spawned at once and give them their angles
        for (int i = 0; i < _spawnAmount; i++)
        {
            PoolObject obj = PoolManager.Instance.Spawn(_poolObject); // Spawns the object with the pool manager

            obj.transform.position = _spawnLocation; // Position is on the spawner

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

                if (objMoveStrategy is SineWaveMovement sineWaveMovement) // Assign the variables for sine waves
                {
                    sineWaveMovement.Initialize(_spawnLocation, worldDirection);

                    sineWaveMovement.Amplitude = _sineAmplitude;
                    sineWaveMovement.Frequency = _sineFrequency;
                    sineWaveMovement.Inverted = _invertSine;
                }
                else if (objMoveStrategy is SpiralMovement spiralMovement) // Assign variables for spirals
                {
                    spiralMovement.Initialize(_spawnLocation, worldDirection);

                    spiralMovement.AngleIncrement = _spiralAngleIncrement;
                    spiralMovement.RateOfGrowth = _rateOfSpiralGrowth;
                    spiralMovement.InitialPosition = _spawnLocation;
                }
                else if (objMoveStrategy is HomingMovement homingMovement) // Assign variables for homing objects
                {
                    homingMovement.Initialize(_spawnLocation, _homingTarget);

                    homingMovement.TargetToFollow = _homingTarget;
                }
                else
                {
                    objMoveStrategy.Initialize(_spawnLocation, worldDirection);
                }
            }
        }
    }
}