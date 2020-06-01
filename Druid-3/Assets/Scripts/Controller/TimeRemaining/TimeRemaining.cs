using System;
using Interface;


namespace Controller.TimeRemaining
{
    public sealed class TimeRemaining : ITimeRemaining
    {
        #region Properties

        public Action Method { get; }
        public bool IsRepeating { get; }
        public float Time { get; }
        public float CurrentTime { get; set; }

        #endregion


        #region Methods

        public TimeRemaining(Action method, float time, bool isRepeting = false)
        {
            Method = method;
            Time = time;
            CurrentTime = time;
            IsRepeating = isRepeting;
        }

        #endregion
    }
}