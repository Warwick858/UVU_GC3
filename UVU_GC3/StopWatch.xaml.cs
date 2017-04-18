using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace UVU_GC3
{
    public sealed partial class StopWatch : UserControl
    {
        //Declare Class Vars:
        //private static int ticked = 1;
        //private static int toTick = 59;
        private static string _defaultTime = "00:00:00";

        private DispatcherTimer _timer;
        private DateTime _startTime = DateTime.MinValue;
        public TimeSpan _currentElapsed = TimeSpan.Zero;
        public TimeSpan _totalElapsed = TimeSpan.Zero;
        private bool _timerRunning = false;
        private static TimeSpan _timeSinceStart;

        /// <summary>
        /// Default Constructor - To initialize components
        /// </summary>
        public StopWatch()
        {
            this.InitializeComponent();
        } // end default constructor

        /// <summary>
        /// InitTimer - To init timer object and start it
        /// </summary>
        public void InitTimer()
        {
            //Init DispatcherTimer, set handler & interval, start timer
            _timer = new DispatcherTimer();
            _timer.Tick += Timer_Tick;
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Start();
            //this.DataContext = this._currentElapsed;

            //Display time of zero - 00:00:00
            TimeTxt.Text = _currentElapsed.ToString();
        } // end method InitTimer()

        /// <summary>
        /// Timer_Tick - To increment the timer by 1 second, and display the time
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">object</param>
        private void Timer_Tick(object sender, object e)
        {
            ProcessTick();
        } // end method Timer_Tick()

        /// <summary>
        /// ProcessTick() - Helper method for Timer_Tick()
        /// </summary>
        private void ProcessTick()
        {
            //
            _timeSinceStart = DateTime.Now - _startTime;
            _timeSinceStart = new TimeSpan(_timeSinceStart.Hours,
                                            _timeSinceStart.Minutes,
                                            _timeSinceStart.Seconds);

            //
            _currentElapsed = _timeSinceStart + _totalElapsed;
            TimeTxt.Text = _currentElapsed.ToString();
        } // end method ProcessTick()

        /// <summary>
        /// StartStopBtn_Click - To start or stop the timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartStopBtn_Click(object sender, RoutedEventArgs e)
        {
            //If timer ISN'T running
            if (!_timerRunning)
            {
                //Initialize timer
                InitTimer();

                //Set start time to now
                _startTime = DateTime.Now;

                //Store total elapsed time so far
                _totalElapsed = _currentElapsed;

                //Start timer & set flag
                _timer.Start();
                _timerRunning = true;
            } // end if
            else // If timer IS running
            {
                TimeTxt.Text = _currentElapsed.ToString();
                //_timer.Start();

                //Stop timer & set flag
                _timer.Stop();
                _timerRunning = false;
            } // end else
        } // end method StartStopBtn_Click()

        /// <summary>
        /// RestartBtn_Click - To stop the timer and zero out the displayed time
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">RoutedEventArgs</param>
        private void RestartBtn_Click(object sender, RoutedEventArgs e)
        {
            //Stop timer and set flag
            _timer.Stop();
            _timerRunning = false;

            //Reset total and current TimeSpans
            _totalElapsed = TimeSpan.Zero;
            _currentElapsed = TimeSpan.Zero;

            //Display default time
            TimeTxt.Text = _defaultTime;
        } // end method RestartBtn_Click()

        /// <summary>
        /// SeekBckBtn_Click - To repeatedly decrement timer tick and update displayed time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeekBckBtn_Click(object sender, RoutedEventArgs e)
        {
            //
            _timer.Stop();
            _timerRunning = false;

            //If timer is greater than zero
            if (_currentElapsed.Seconds > 0)
            {
                //_currentElapsed = _currentElapsed.Add(DateTime.Now.AddSeconds(1).To);


                //
                _timeSinceStart = DateTime.Now.AddSeconds(3) - _startTime;
                //timeSinceStart = new TimeSpan(timeSinceStart.Hours,
                //                                timeSinceStart.Minutes,
                //                                timeSinceStart.Seconds);

                //
                _currentElapsed = _timeSinceStart + _totalElapsed;
                TimeTxt.Text = _currentElapsed.ToString();

                _timer.Start();
            } // end if
        } // end method SeekBckBtn_Click()

        /// <summary>
        /// SeekFwdBtn_Click - To repeatedly increment timer tick and update displayed time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeekFwdBtn_Click(object sender, RoutedEventArgs e)
        {
            //
            _timer.Stop();
            _timerRunning = false;

            TimeSpan currDisplayed = new TimeSpan(int.Parse(TimeTxt.Text.Substring(0, 2)), int.Parse(TimeTxt.Text.Substring(3, 2)), int.Parse(TimeTxt.Text.Substring(6, 2)));

            currDisplayed = currDisplayed.Add(new TimeSpan(0, 0, 10));

            TimeTxt.Text = currDisplayed.ToString();

            _timeSinceStart = new TimeSpan(currDisplayed.Hours, currDisplayed.Minutes, currDisplayed.Seconds);

            _currentElapsed = _timeSinceStart + _totalElapsed;

            //timeee.Add(new TimeSpan(0, 0, 1));

            //timeSinceStart.Add(new TimeSpan(0, 0, 1)) - _startTime;
            //timeSinceStart = DateTime.Now.AddMilliseconds(1000) - _startTime; 
            //timeSinceStart = new TimeSpan(timeSinceStart.Hours,
            //                                timeSinceStart.Minutes,
            //                                timeSinceStart.Seconds);

            //
            //_currentElapsed = timeSinceStart + _totalElapsed;
            //TimeTxt.Text = _currentElapsed.ToString();

            //_timer.Start();
        } // end method SeekFwdBtn_Click()

        private void SeekFwdBtn_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            //_timer.Start();
        }
    } // end class StopWatch
} // end namespace UVU_GC3
