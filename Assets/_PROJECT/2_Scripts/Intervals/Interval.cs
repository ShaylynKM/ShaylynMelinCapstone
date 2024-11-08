using System;
using UnityEngine;
using UnityEngine.Events;


    /// <summary>
    /// Each interval is either going to be discrete or continuous (sub classes), and all will execute commands
    /// Interval ends based on some sort of condition (a Predicate function)
    /// </summary>
    public abstract class Interval : MonoBehaviour
    {
        protected bool _isActive = false;
        public event Action Executed;
        public event Action Complete;
        [SerializeField] private bool startOnSpawn = false;
        protected float _timeBegan;
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
                if(_isActive) _timeBegan = Time.time;
            }
        }

        protected virtual void Start()
        {
            if (startOnSpawn)
                Begin();
        }
        public virtual void Begin()
        {
            IsActive = true;
        }

        public virtual void Stop()
        {
            IsActive = false;
        }
        protected virtual void Update()
        {
            if (!_isActive)
                return;
        }

        protected virtual void Execute() //all intervals have to have to be able to execute
        {
        }

        protected virtual void Completed()
        {
            Complete?.Invoke();
        }
    }
