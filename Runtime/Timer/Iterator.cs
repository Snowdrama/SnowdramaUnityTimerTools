using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowdrama.Timer
{
    /// <summary>
    /// A tool for calling a function every specific amount of time automatically
    /// 
    /// Example: new Itterator(1.0f) will call OnItterate every second forever
    /// 
    /// Example: new Itterator(1.0f, 10.0f) will call OnItterate 
    /// every second until 10 seconds is reached
    /// then it will call OnDurationCompltete and Stop
    /// </summary>
    public class Iterator
    {
        /// <summary>
        /// Called only when the duration of the itterator is complete
        /// </summary>
        public event Action OnDurationComplete;

        /// <summary>
        /// Called whenever Stop is called and when the duration is reached.
        /// </summary>
        public event Action OnStop;

        /// <summary>
        /// Called whenever the itteration time is greater than the set time.
        /// </summary>
        public event Action OnIterate; //called each timer iteration

        /// <summary>
        /// Called when the itterator is started
        /// </summary>
        public event Action OnStart;

        [Header("Itterate")]
        [SerializeField] private float _iterateTarget;
        [SerializeField] private float _currentItterationTime;
        public float CurrentTime { get { return _currentItterationTime; } }

        [Header("Duration")]
        [SerializeField, Tooltip("How long to iterate for before stopping, less than 0 = forever")] private float _durationTarget; //how long to iterate for before stopping, less than 0 = forever
        [SerializeField] private float _currentDuration;
        public float CurrentDuration { get { return _currentDuration; } }

       
        [SerializeField] private bool _active;
        public bool Active { get { return _active; } }
        [SerializeField] private bool _paused;
        public bool Paused { get { return _paused; } }
        [SerializeField, Tooltip("Should the itterator initialize started")] private bool autoStart;

        public Iterator(float iterateTime, float duration = -1, bool autoStart = true)
        {
            _currentItterationTime = 0;
            this._iterateTarget = iterateTime;
            this._durationTarget = duration;
            this.autoStart = autoStart;
            if (this.autoStart)
            {
                _active = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iterateTime"></param>
        public void SetNewItterateTime(float iterateTime)
        {
            this._iterateTarget = iterateTime;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deltaTime"></param>
        public void UpdateTime(float deltaTime)
        {
            if (_active && !_paused)
            {
                _currentItterationTime += deltaTime;
                _currentDuration += deltaTime;

                if (_currentItterationTime > _iterateTarget)
                {
                    OnIterate?.Invoke();
                    _currentItterationTime = 0;
                }
                if (_durationTarget > 0)
                {
                    if (_currentDuration > _durationTarget)
                    {
                        OnDurationComplete?.Invoke();
                        Stop();
                    }
                }
            }
        }
        //starts or resumes the timer depending on if Stop or Pause was used
        public void Start()
        {
            if (!_active)
            {
                _active = true;
                _paused = false;
                OnStart?.Invoke();
                return;
            }

            //if we were active but paused, Start will also unpause
            if (_paused)
            {
                Unpause();
            }
        }
        //stops the timer, and resets the current time to 0
        public void Stop()
        {
            if (_active)
            {
                _active = false;
                _paused = false;
                OnStop?.Invoke();
                _currentItterationTime = 0;
                _currentDuration = 0;
            }
        }

        /// <summary>
        /// stops the timer and allows resuming from the current time
        /// </summary>
        public void Pause()
        {
            _paused = true;
        }

        /// <summary>
        /// unpauses the timer if pause was called
        /// </summary>
        public void Unpause()
        {
            _paused = false;
        }

        /// <summary>
        /// Gets the percentage from 0 to 1 as a percentage of current time to the itterators's duration
        /// </summary>
        /// <returns>A float from 0 to 1</returns>
        public float GetPercentageComplete()
        {
            return _currentDuration / _durationTarget;
        }
    }
}