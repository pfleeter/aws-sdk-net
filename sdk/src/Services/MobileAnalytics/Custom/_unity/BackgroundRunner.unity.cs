/*
 * Copyright 2012-2013 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 * 
 *  http://aws.amazon.com/apache2.0
 * 
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;
using Amazon.Runtime.Internal.Util;
using Amazon.Runtime.Internal;


namespace Amazon.MobileAnalytics.MobileAnalyticsManager.Internal
{
    /// <summary>
    /// Amazon mobile analytics background runner.
    /// Background runner periodically sends events to server.
    /// </summary>
    public partial class BackgroundRunner
    {
        private static System.Threading.Thread _thread = null;
        private static volatile bool _shouldStop;
        /// <summary>
        /// Determines if background thread is alive.
        /// </summary>
        /// <returns><c>true</c> If is alive; otherwise, <c>false</c>.</returns>
        private bool IsAlive()
        {
            return _thread != null && _thread.ThreadState != ThreadState.Stopped
                                   && _thread.ThreadState != ThreadState.Aborted
                                   && _thread.ThreadState != ThreadState.AbortRequested;
        }

        /// <summary>
        /// Starts the Mobile Analytics Manager background thread.
        /// </summary>
        public void StartWork()
        {
            lock (_lock)
            {
                if (!IsAlive())
                {
                    _thread = new System.Threading.Thread(DoWork);
                    _thread.Start();
                }
            }
        }

        /// <summary>
        /// Abort the background worker
        /// </summary>
        public static void AbortBackgroundThread()
        {
            _shouldStop = true;
            if (_thread != null)
            {
                _thread.Join();
                _thread = null;
            }
        }

        /// <summary>
        /// Sends Mobile Analytics events to server on background thread.
        /// </summary>
        private void DoWork()
        {
            while (!_shouldStop)
            {
                try
                {
                    _logger.InfoFormat("Mobile Analytics Manager is trying to deliver events in background thread.");

                    IDictionary<string, MobileAnalyticsManager> instanceDictionary = MobileAnalyticsManager.CopyOfInstanceDictionary;
                    foreach (string appId in instanceDictionary.Keys)
                    {
                        MobileAnalyticsManager manager = null;
                        try
                        {
                            manager = MobileAnalyticsManager.GetInstance(appId);
                            manager.BackgroundDeliveryClient.AttemptDelivery();
                        }
                        catch (ThreadAbortException e)
                        {
                            // handle thread aborts more gracefully in unity
                            throw e;
                        }
                        catch (System.Exception e)
                        {
                            _logger.Error(e, "An exception occurred in Mobile Analytics Delivery Client : {0}", e.ToString());

                            if (null != manager)
                            {
                                MobileAnalyticsErrorEventArgs eventArgs = new MobileAnalyticsErrorEventArgs(this.GetType().Name, "An exception occurred when deliverying events to Amazon Mobile Analytics.", e, new List<Amazon.MobileAnalytics.Model.Event>());
                                manager.OnRaiseErrorEvent(eventArgs);
                            }
                        }
                    }
                    Thread.Sleep(BackgroundSubmissionWaitTime * 1000);
                }
                catch(ThreadAbortException)
                {
                    // handle thread aborts more gracefully in unity
                }
                catch (System.Exception e)
                {
                    _logger.Error(e, "An exception occurred in Mobile Analytics Manager.");
                }

                UnityRequestQueue.Instance.ExecuteOnMainThread(() =>
                {
                    if (Application.isEditor && !Application.isPlaying)
                    {
                        AbortBackgroundThread();
                    }
                });

            }
        }
    }
}

