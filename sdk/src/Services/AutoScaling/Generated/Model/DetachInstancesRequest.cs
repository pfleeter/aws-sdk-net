/*
 * Copyright 2010-2014 Amazon.com, Inc. or its affiliates. All Rights Reserved.
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

/*
 * Do not modify this file. This file is generated from the autoscaling-2011-01-01.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.AutoScaling.Model
{
    /// <summary>
    /// Container for the parameters to the DetachInstances operation.
    /// Removes one or more instances from the specified Auto Scaling group.
    /// 
    ///  
    /// <para>
    /// After the instances are detached, you can manage them independently from the rest
    /// of the Auto Scaling group.
    /// </para>
    ///  
    /// <para>
    /// If you do not specify the option to decrement the desired capacity, Auto Scaling launches
    /// instances to replace the ones that are detached.
    /// </para>
    ///  
    /// <para>
    /// For more information, see <a href="http://docs.aws.amazon.com/AutoScaling/latest/DeveloperGuide/detach-instance-asg.html">Detach
    /// EC2 Instances from Your Auto Scaling Group</a> in the <i>Auto Scaling Developer Guide</i>.
    /// </para>
    /// </summary>
    public partial class DetachInstancesRequest : AmazonAutoScalingRequest
    {
        private string _autoScalingGroupName;
        private List<string> _instanceIds = new List<string>();
        private bool? _shouldDecrementDesiredCapacity;

        /// <summary>
        /// Gets and sets the property AutoScalingGroupName. 
        /// <para>
        /// The name of the group.
        /// </para>
        /// </summary>
        public string AutoScalingGroupName
        {
            get { return this._autoScalingGroupName; }
            set { this._autoScalingGroupName = value; }
        }

        // Check to see if AutoScalingGroupName property is set
        internal bool IsSetAutoScalingGroupName()
        {
            return this._autoScalingGroupName != null;
        }

        /// <summary>
        /// Gets and sets the property InstanceIds. 
        /// <para>
        /// One or more instance IDs.
        /// </para>
        /// </summary>
        public List<string> InstanceIds
        {
            get { return this._instanceIds; }
            set { this._instanceIds = value; }
        }

        // Check to see if InstanceIds property is set
        internal bool IsSetInstanceIds()
        {
            return this._instanceIds != null && this._instanceIds.Count > 0; 
        }

        /// <summary>
        /// Gets and sets the property ShouldDecrementDesiredCapacity. 
        /// <para>
        /// If <code>True</code>, the Auto Scaling group decrements the desired capacity value
        /// by the number of instances detached.
        /// </para>
        /// </summary>
        public bool ShouldDecrementDesiredCapacity
        {
            get { return this._shouldDecrementDesiredCapacity.GetValueOrDefault(); }
            set { this._shouldDecrementDesiredCapacity = value; }
        }

        // Check to see if ShouldDecrementDesiredCapacity property is set
        internal bool IsSetShouldDecrementDesiredCapacity()
        {
            return this._shouldDecrementDesiredCapacity.HasValue; 
        }

    }
}