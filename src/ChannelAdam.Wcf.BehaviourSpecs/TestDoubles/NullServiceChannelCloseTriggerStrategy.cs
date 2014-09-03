﻿//-----------------------------------------------------------------------
// <copyright file="NullServiceChannelCloseTriggerStrategy.cs">
//     Copyright (c) 2014 Adam Craven. All rights reserved.
// </copyright>
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//-----------------------------------------------------------------------

namespace ChannelAdam.Wcf.BehaviourSpecs.TestDoubles
{
    using System;
    using System.ServiceModel;

    using ChannelAdam.ServiceModel;

    /// <summary>
    /// A strategy that will never trigger the closing of the service channel when there is an exception calling the service operation.
    /// </summary>
    /// <remarks>
    /// This effectively makes the service consumer act like WCF out of the box and not close the channnel.
    /// </remarks>
    public class NullServiceChannelCloseTriggerStrategy : IServiceChannelCloseTriggerStrategy
    {
        /// <summary>
        /// Determines if the channel should be closed, based on the channel's state and the exception that occurred.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="exception">The exception.</param>
        /// <returns><c>true</c> if the channel should be closed, otherwise <c>false</c>.</returns>
        public bool ShouldCloseChannel(ICommunicationObject channel, Exception exception)
        {
            return false;
        }
    }
}
