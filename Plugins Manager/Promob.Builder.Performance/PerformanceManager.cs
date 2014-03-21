
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Promob.Builder.Performance
{
    public class PerformanceManager
    {
        #region External

        [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod", SetLastError = true)]
        private static extern uint TimeBeginPeriod(uint uMilliseconds);

        [DllImport("winmm.dll", EntryPoint = "timeEndPeriod", SetLastError = true)]
        private static extern uint TimeEndPeriod(uint uMilliseconds);

        #endregion

        #region Singleton

        private PerformanceManager()
        { }

        private static PerformanceManager _instance;
        public static PerformanceManager Instance
        {
            get
            {
                if (PerformanceManager._instance == null)
                    PerformanceManager._instance = new PerformanceManager();

                return PerformanceManager._instance;
            }
        }

        #endregion

        #region Attributes and Properties

        private bool _initialized;

        private Stopwatch _runningTime;
        public Stopwatch RunningTime
        {
            get
            {
                if (this._runningTime == null)
                    this._runningTime = new Stopwatch();

                return _runningTime;
            }
        }

        private Take _lastTake;
        public Take LastTake
        {
            get { return _lastTake; }
            set { _lastTake = value; }
        }

        private List<Take> _takes;
        public List<Take> Takes
        {
            get { return _takes; }
        }

        #endregion

        #region Public Methods

        public void Initialize()
        {
            TimeBeginPeriod(1);
            this._takes = new List<Take>();
            this._initialized = true;
            this._runningTime = new Stopwatch();
            Debug.WriteLine("PerformanceManager:Initialized");
        }

        public void Take()
        {
            TimeSpan at = this._runningTime.Elapsed - this._lastTake.At;
            this._takes.Add(new Take(string.Format("{0}", at.TotalSeconds), at));
            this._lastTake.At = this._runningTime.Elapsed;
        }

        public void Take(string description)
        {
            TimeSpan at = this._runningTime.Elapsed - this._lastTake.At;
            this._takes.Add(new Take(string.Format("{0}: {1}", description, at.TotalSeconds), at));
            this._lastTake.At = this._runningTime.Elapsed;
        }

        public void Restart()
        {
            Debug.WriteLine(string.Format("Restarted at: {0}", this._runningTime.Elapsed.TotalSeconds));
            this._takes.Clear();
            this._runningTime.Reset();
            this._lastTake = new Take();
            this._runningTime.Start();
        }

        public void WriteToDebugOutput()
        {

            double sum = 0;
            foreach (Take take in this.Takes)
            {
                sum += take.At.TotalSeconds;
                Debug.WriteLine(take.Description);
                Console.WriteLine(take.Description);
            }

            Debug.WriteLine(string.Format("Elapsed Time: {0}", sum));
            Console.WriteLine(string.Format("Elapsed Time: {0}", sum));
        }

        public void Terminate()
        {
            this._runningTime.Stop();
            TimeEndPeriod(1);
        }

        #endregion
    }
}