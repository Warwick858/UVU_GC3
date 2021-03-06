﻿using System;
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
        private DispatcherTimer _timer;
        private DateTime _startTime = DateTime.MinValue;
        public TimeSpan _currentElapsed = TimeSpan.Zero;
        public TimeSpan _totalElapsed = TimeSpan.Zero;
        private static TimeSpan _timeSinceStart;
        //private static TimeSpan _manipulatedTime;
        private bool _timerRunning = false;
        private static string _defaultTime = "00:00:00";

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
            //Calc difference between now and start time
            _timeSinceStart = DateTime.Now - _startTime;
            _timeSinceStart = new TimeSpan(_timeSinceStart.Hours,
                                            _timeSinceStart.Minutes,
                                            _timeSinceStart.Seconds);

            //Set currentElapsed to sum of timeSinceStart and totalElapsed
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
        /// SeekBckBtn_Click - To repeatedly decrement total elapsed time and update displayed time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeekBckBtn_Click(object sender, RoutedEventArgs e)
        {
            //If timer is greater than zero
            if (_currentElapsed.Seconds > 0)
            {
                //Stop timer and set flag
                _timer.Stop();
                _timerRunning = false;

                //Create new TimeSpan using currently displayed time
                TimeSpan currDisplayed = new TimeSpan(int.Parse(TimeTxt.Text.Substring(0, 2)),
                                                        int.Parse(TimeTxt.Text.Substring(3, 2)),
                                                        int.Parse(TimeTxt.Text.Substring(6, 2)));

                //Subtract 10 seconds to currently displayed time and display the new time
                currDisplayed = currDisplayed.Subtract(new TimeSpan(0, 0, 10));
                TimeTxt.Text = currDisplayed.ToString();

                //Add fast forward changes to main TimeSpan mechanism
                _totalElapsed = new TimeSpan(currDisplayed.Hours, currDisplayed.Minutes, currDisplayed.Seconds);
                _currentElapsed = _totalElapsed;

                //Start timer and set flag
                _timer.Start();
                _timerRunning = true;
            } // end if
        } // end method SeekBckBtn_Click()

        /// <summary>
        /// SeekFwdBtn_Click - To repeatedly increment total elapsed time and update displayed time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeekFwdBtn_Click(object sender, RoutedEventArgs e)
        {
            //Stop timer and set flag
            _timer.Stop();
            _timerRunning = false;

            //Create new TimeSpan using currently displayed time
            TimeSpan currDisplayed = new TimeSpan(int.Parse(TimeTxt.Text.Substring(0, 2)), 
                                                    int.Parse(TimeTxt.Text.Substring(3, 2)), 
                                                    int.Parse(TimeTxt.Text.Substring(6, 2)));

            //Add 10 seconds to currently displayed time and display the new time
            currDisplayed = currDisplayed.Add(new TimeSpan(0, 0, 10));
            TimeTxt.Text = currDisplayed.ToString();

            //Add fast forward changes to main TimeSpan mechanism
            //_timeSinceStart = new TimeSpan(currDisplayed.Hours, currDisplayed.Minutes, currDisplayed.Seconds);
            _totalElapsed = new TimeSpan(currDisplayed.Hours, currDisplayed.Minutes, currDisplayed.Seconds);

            //_currentElapsed = _timeSinceStart;// + _totalElapsed;
            _currentElapsed = _totalElapsed;

            //Start timer and set flag
            _timer.Start();
            _timerRunning = true;
        } // end method SeekFwdBtn_Click()
    } // end class StopWatch
} // end namespace UVU_GC3
