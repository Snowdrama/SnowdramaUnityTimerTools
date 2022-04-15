using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowdrama.Timer
{
    [System.Serializable]
    public class Timer
    {
        public event Action OnComplete;
        public event Action OnRestart; //called when the timer is set back to 0 from max
        public event Action OnStart; //called when the timer is started after stopping or on first start
        [SerializeField] private float currentTime;
        [SerializeField] private float duration;
        [SerializeField] public bool Active { get { return active; } }
        [SerializeField] private bool active;
        [SerializeField] private bool autoRestart;
        public Timer(float time, bool autoStart = false, bool autoRestart = false)
        {
            if (time <= 0)
            {
                time = 0; //prevent negative
                Debug.LogError("Time for timer needs to be greater than 0!");
            }
            duration = time;
            currentTime = 0;
            active = autoStart;
            this.autoRestart = autoRestart;
        }

        /// <summary>
        /// Updates the timer based on time that has passed since last updated. Will automatically invoke
        /// Action OnComplete if the currentTime elapses the timer duration. If AutoRestart is set OnRestart
        /// will be called and the timer will automacially reset the timer to 0 and continue.
        ///  
        /// Note: if the delta time ends up longer than 2 times the duration and auto restart is enabled OnRestart
        /// will be called each update until the currentTime becomes less than the timer duration. 
        /// </summary>
        /// <param name="deltaTime">the delta of time passed since the last timer update</param>
        public void UpdateTime(float deltaTime)
        {
            if (active)
            {
                currentTime += deltaTime;
                if (currentTime >= duration && active)
                {
                    currentTime -= duration;
                    active = false;
                    if (autoRestart)
                    {
                        OnComplete?.Invoke();
                        RestartTimer();
                    }
                    else
                    {
                        currentTime = 0;
                        OnComplete?.Invoke();
                    }
                }
            }
        }
        /// <summary>
        /// Starts or resumes the timer depending on if Stop or Pause was used
        /// </summary>
        public void Start()
        {
            if (!active)
            {
                OnStart?.Invoke();
                //the timer was paused so resume
                active = true;
            }
        }
        /// <summary>
        /// Stops the timer, and resets the current time to 0
        /// </summary>
        public void Stop()
        {
            active = false;
            currentTime = 0;
        }

        /// <summary>
        /// Stops the timer and allows resuming from the current time
        /// </summary>
        public void Pause()
        {
            active = false;
        }

        /// <summary>
        /// returns the current time in seconds
        /// </summary>
        /// <returns>A float representing the time elapsed</returns>
        public float GetTime()
        {
            return currentTime;
        }

        /// <summary>
        /// returns the percentage from 0 to 1 as a percentage of current time to the timer's duration
        /// </summary>
        /// <returns>A Float from 0 to 1 representing the total time</returns>
        public float GetPercentageComplete()
        {
            return currentTime / duration;
        }

        /// <summary>
        /// returns the amount of time remaining until the timer ends
        /// </summary>
        /// <returns>A Float representing time remaining</returns>
        public float GetTimeRemaining()
        {
            return duration - currentTime;
        }

        /// <summary>
        /// start the timer over, allow passing in a new time.
        /// </summary>
        /// <param name="newTime"></param>
        public void RestartTimer(float newTime = -1)
        {
            active = true;
            OnRestart?.Invoke();
            if (newTime > 0)
            {
                duration = newTime;
            }
        }

        public void SetNewTime(float newTime)
        {
            duration = newTime;
        }
    }
}