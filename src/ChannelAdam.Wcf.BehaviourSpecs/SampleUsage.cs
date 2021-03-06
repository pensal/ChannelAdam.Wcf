﻿//-----------------------------------------------------------------------
// <copyright file="SampleUsage.cs">
//     Copyright (c) 2014-2016 Adam Craven. All rights reserved.
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

namespace ChannelAdam.Wcf.BehaviourSpecs
{
    using System;
    using System.ServiceModel;
    using ChannelAdam.ServiceModel;
    using ChannelAdam.Wcf.BehaviourSpecs.TestDoubles;

    using Microsoft.Practices.TransientFaultHandling;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;
    using SampleServiceReference;

    /// <summary>
    /// Sample typical usage of the ServiceConsumer.
    /// </summary>
    [TestClass]
    public class SampleUsage
    {
        ////[TestMethod]
        ////public void Sample_Level100_BasicUsage_OperationsProperty_RealService()
        ////{
        ////    using (var service = ServiceConsumerFactory.Create<ISampleService>("BasicHttpBinding_ISampleService"))
        ////    {
        ////        try
        ////        {
        ////            string actual = service.Operations.SampleOperation(1);
        ////            Console.WriteLine("Actual: " + actual);
        ////            Assert.AreEqual("You entered: 1", actual);

        ////            return;
        ////        }
        ////        // catch (FaultException<MyBusinessLogicType> fe)
        ////        catch (FaultException fe)
        ////        {
        ////            Console.WriteLine("Service operation threw a fault: " + fe.ToString());
        ////        }
        ////        catch (Exception ex)
        ////        {
        ////            Console.WriteLine("Technical error occurred while calling the service operation: " + ex.ToString());
        ////        }

        ////        Assert.Fail("Service operation was not successfully called");
        ////    }
        ////}

        [TestMethod]
        public void Sample_Level100_BasicUsage_OperationsProperty()
        {
            //using (var service = ServiceConsumerFactory.Create<IFakeService>("BasicHttpBinding_IFakeService"))
            using (var service = ServiceConsumerFactory.Create<IFakeService>(() => new FakeServiceClient()))
            {
                try
                {
                    int actual = service.Operations.AddIntegers(1, 1);
                    Console.WriteLine("Actual: " + actual);
                    Assert.AreEqual(2, actual);

                    return;
                }
                // catch (FaultException<MyBusinessLogicType> fe)
                catch (FaultException fe)
                {
                    Console.WriteLine("Service operation threw a fault: " + fe.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Technical error occurred while calling the service operation: " + ex.ToString());
                }

                Assert.Fail("Service operation was not successfully called");
            }
        }

        [TestMethod]
        public void Sample_Level100_BasicUsage_ConsumeMethod()
        {
            using (var service = ServiceConsumerFactory.Create<IFakeService>(() => new FakeServiceClient()))
            {
                IOperationResult<int> result = service.Consume(operation => operation.AddIntegers(1, 1));

                if (result.HasNoException)
                {
                    int actual = result.Value;
                    Console.WriteLine("Actual: " + actual);
                    Assert.AreEqual(2, actual);
                }
                else
                {
                    // if (result.HasFaultExceptionOfType<MyBusinessLogicException>())
                    if (result.HasFaultException)
                    {
                        Console.WriteLine("Service operation threw a fault: " + result.Exception.ToString());
                    }
                    else
                    {
                        Console.WriteLine("Technical error occurred while calling the service operation: " + result.Exception.ToString());
                    }

                    Assert.Fail("Service operation call threw an exception");
                }
            }
        }

        [TestMethod]
        public void Sample_Level100_BasicUsage_MultipleCalls()
        {
            using (var service = ServiceConsumerFactory.Create<IFakeService>(() => new FakeServiceClient()))
            {
                IOperationResult<int> result = service.Consume(operation => operation.AddIntegers(1, 1));
                AssertOperationResult(2, result);

                // Even if the channel had a communication exception and went into the fault state or was aborted,
                // you can still use the service consumer!

                result = service.Consume(operation => operation.AddIntegers(1, 3));
                AssertOperationResult(4, result);
            }
        }

        private void AssertOperationResult<T>(T expected, IOperationResult<T> result)
        {
            if (result.HasNoException)
            {
                T actual = result.Value;
                Console.WriteLine("Actual: " + actual);
                Assert.AreEqual(expected, actual);
            }
            else
            {
                // if (result.HasFaultExceptionOfType<MyBusinessLogicException>())
                if (result.HasFaultException)
                {
                    Console.WriteLine("Service operation threw a fault: " + result.Exception.ToString());
                }
                else
                {
                    Console.WriteLine("Technical error occurred while calling the service operation: " + result.Exception.ToString());
                }

                Assert.Fail("Service operation call threw an exception");
            }
        }

        [TestMethod]
        public void Sample_Level200_Static_DefaultExceptionBehaviourStrategy()
        {
            // Apply this exception behaviour strategy for all created service consumer instances.
            // By default, out of the box, the default is a StandardErrorServiceConsumerExceptionBehaviourStrategy.
            ServiceConsumerFactory.DefaultExceptionBehaviourStrategy = new StandardOutServiceConsumerExceptionBehaviourStrategy();

            using (var service = ServiceConsumerFactory.Create<IFakeService>(() => new FakeServiceClient()))
            {
                try
                {
                    int actual = service.Operations.AddIntegers(1, 1);

                    Console.WriteLine("Actual: " + actual);
                    Assert.AreEqual(2, actual);

                    return;
                }
                catch (FaultException fe)
                {
                    Console.WriteLine("Service operation threw a fault: " + fe.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Technical error occurred while calling the service operation: " + ex.ToString());
                }

                Assert.Fail("Service operation was not successfully called");
            }
        }

        [TestMethod]
        public void Sample_Level200_Instance_ExceptionBehaviourStrategy()
        {
            // Apply the exception handling strategy only for this created service consumer instance
            using (var service = ServiceConsumerFactory.Create<IFakeService>(
                                    () => new FakeServiceClient(),
                                    new StandardOutServiceConsumerExceptionBehaviourStrategy()))
            {
                try
                {
                    int actual = service.Operations.AddIntegers(1, 1);

                    Console.WriteLine("Actual: " + actual);
                    Assert.AreEqual(2, actual);

                    return;
                }
                catch (FaultException fe)
                {
                    Console.WriteLine("Service operation threw a fault: " + fe.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Technical error occurred while calling the service operation: " + ex.ToString());
                }

                Assert.Fail("Service operation was not successfully called");
            }
        }

        [TestMethod]
        public void Sample3_Level300_AutomaticRetry_Manual_Naive_UsingOperationProperty()
        {
            int retryCount = 1;
            Exception lastException = null;

            using (var service = ServiceConsumerFactory.Create<IFakeService>(() => new FakeServiceClient()))
            {
                while (retryCount > 0)
                {
                    Console.WriteLine("#### Retry count: " + retryCount);

                    try
                    {
                        int actual = service.Operations.AddIntegers(1, 1);

                        Console.WriteLine("Actual: " + actual);
                        Assert.AreEqual(2, actual);

                        return;
                    }
                    catch (FaultException fe)
                    {
                        lastException = fe;
                        Console.WriteLine("Service operation threw a fault: " + fe.ToString());
                    }
                    catch (Exception ex)
                    {
                        lastException = ex;
                        Console.WriteLine("Technical error occurred while calling the service operation: " + ex.ToString());
                    }

                    retryCount--;
                }
            }

            Assert.Fail("Service operation was not successfully called");
        }

        [TestMethod]
        public void Sample3_Level300_AutomaticRetry_ManuallyUsingMicrosoftTransientFaultHandling_UsingOperationsProperty()
        {
            using (var service = ServiceConsumerFactory.Create<IFakeService>(() => new FakeServiceClient()))
            {
                try
                {
                    int actual = 0;

                    var microsoftRetryStrategy = new Incremental(5, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2));
                    var microsoftRetryPolicy = new RetryPolicy<SoapFaultWebServiceTransientErrorDetectionStrategy>(microsoftRetryStrategy);

                    microsoftRetryPolicy.ExecuteAction(() =>
                    {
                        actual = service.Operations.AddIntegers(1, 1);
                    });

                    Console.WriteLine("Actual: " + actual);
                    Assert.AreEqual(2, actual);

                    return;
                }
                catch (FaultException fe)
                {
                    Console.WriteLine("Service operation threw a fault: " + fe.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Technical error occurred while calling the service operation: " + ex.ToString());
                }

                Assert.Fail("Service operation was not successfully called");
            }
        }

        [TestMethod]
        public void Sample3_Level300_AutomaticRetry_StaticServiceConsumerRetryPolicy_UsingMicrosoftTransientFaultHandling_UsingOperationsProperty()
        {
            var microsoftRetryStrategy = new Incremental(5, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2));
            var microsoftRetryPolicy = new RetryPolicy<SoapFaultWebServiceTransientErrorDetectionStrategy>(microsoftRetryStrategy);
            ServiceConsumerFactory.DefaultRetryPolicy = microsoftRetryPolicy.ForServiceConsumer(); // extension method
            // or ServiceConsumerFactory.DefaultRetryPolicy = new ChannelAdam.TransientFaultHandling.RetryPolicyAdapter(microsoftRetryPolicy);
            // or ServiceConsumerFactory.DefaultRetryPolicy = (ChannelAdam.TransientFaultHandling.IRetryPolicyFunction)microsoftRetryPolicy;

            using (var service = ServiceConsumerFactory.Create<IFakeService>(() => new FakeServiceClient()))
            {
                try
                {
                    int actual = service.Operations.AddIntegers(1, 1);

                    Console.WriteLine("Actual: " + actual);
                    Assert.AreEqual(2, actual);

                    return;
                }
                catch (FaultException fe)
                {
                    Console.WriteLine("Service operation threw a fault: " + fe.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Technical error occurred while calling the service operation: " + ex.ToString());
                }

                Assert.Fail("Service operation was not successfully called");
            }
        }

        [TestMethod]
        public void Sample3_Level300_AutomaticRetry_ServiceConsumerRetryPolicy_UsingMicrosoftTransientFaultHandling_UsingOperationsProperty()
        {
            var microsoftRetryStrategy = new Incremental(5, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2));
            var microsoftRetryPolicy = new RetryPolicy<SoapFaultWebServiceTransientErrorDetectionStrategy>(microsoftRetryStrategy);

            var serviceConsumerRetryPolicy = microsoftRetryPolicy.ForServiceConsumer(); // extension method
            // or var serviceConsumerRetryPolicy = new ChannelAdam.TransientFaultHandling.RetryPolicyAdapter(microsoftRetryPolicy);
            // or var serviceConsumerRetryPolicy = (ChannelAdam.TransientFaultHandling.IRetryPolicyFunction)microsoftRetryPolicy;
            using (var service = ServiceConsumerFactory.Create<IFakeService>(() => new FakeServiceClient(), serviceConsumerRetryPolicy))
            {
                try
                {
                    int actual = service.Operations.AddIntegers(1, 1);

                    Console.WriteLine("Actual: " + actual);
                    Assert.AreEqual(2, actual);

                    return;
                }
                catch (FaultException fe)
                {
                    Console.WriteLine("Service operation threw a fault: " + fe.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Technical error occurred while calling the service operation: " + ex.ToString());
                }

                Assert.Fail("Service operation was not successfully called");
            }
        }

        [TestMethod]
        public void Sample3_Level300_AutomaticRetry_StaticServiceConsumerRetryPolicy_UsingMicrosoftTransientFaultHandling_WithConsumeMethod()
        {
            var microsoftRetryStrategy = new Incremental(5, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2));
            var microsoftRetryPolicy = new RetryPolicy<SoapFaultWebServiceTransientErrorDetectionStrategy>(microsoftRetryStrategy);
            ServiceConsumerFactory.DefaultRetryPolicy = microsoftRetryPolicy.ForServiceConsumer(); // extension method
            // or ServiceConsumerFactory.DefaultRetryPolicy = new ChannelAdam.TransientFaultHandling.RetryPolicyAdapter(microsoftRetryPolicy);
            // or ServiceConsumerFactory.DefaultRetryPolicy = (ChannelAdam.TransientFaultHandling.IRetryPolicyFunction)microsoftRetryPolicy;

            using (var service = ServiceConsumerFactory.Create<IFakeService>(() => new FakeServiceClient()))
            {
                var result = service.Consume(operation => operation.AddIntegers(1, 1));

                if (result.HasNoException)
                {
                    Console.WriteLine("Actual: " + result.Value);
                    Assert.AreEqual(2, result.Value);
                }
                else
                {
                    if (result.HasFaultException)
                    {
                        Console.WriteLine("Service operation threw a fault: " + result.Exception.ToString());
                    }
                    else if (result.HasException)
                    {
                        Console.WriteLine("Technical error occurred while calling the service operation: " + result.Exception.ToString());
                    }

                    Assert.Fail("Service operation was not successfully called");
                }
            }
        }

        //public class AlwaysTransientErrorDetectionStrategy : ITransientErrorDetectionStrategy
        //{
        //    /// <summary>
        //    /// Determines whether the specified exception is a transient failure that could be retried.
        //    /// </summary>
        //    /// <param name="ex">The exception.</param>
        //    /// <returns>
        //    ///   <c>true</c> if the exception is a transient failure that could be retried; otherwise <c>false</c>.
        //    /// </returns>
        //    public bool IsTransient(Exception ex)
        //    {
        //        return true;
        //    }
        //}

        //[TestMethod]
        //public async Task Sample3_Level300_AutomaticRetry_StaticServiceConsumerRetryPolicy_UsingMicrosoftTransientFaultHandling_WithConsumeAsyncMethod()
        //{
        //    ServiceConsumerFactory.DefaultExceptionBehaviourStrategy = new StandardOutServiceConsumerExceptionBehaviourStrategy();

        //    var microsoftRetryStrategy = new Incremental(5, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2));
        //    var microsoftRetryPolicy = new RetryPolicy<AlwaysTransientErrorDetectionStrategy>(microsoftRetryStrategy);
        //    ServiceConsumerFactory.DefaultRetryPolicy = microsoftRetryPolicy.ForServiceConsumer(); // extension method
        //    // or ServiceConsumerFactory.DefaultRetryPolicy = new ChannelAdam.TransientFaultHandling.RetryPolicyAdapter(microsoftRetryPolicy);
        //    // or ServiceConsumerFactory.DefaultRetryPolicy = (ChannelAdam.TransientFaultHandling.IRetryPolicyFunction)microsoftRetryPolicy;

        //    using (var service = ServiceConsumerFactory.Create<ISampleService>("BasicHttpBinding_ISampleService"))
        //    //using (var service = ServiceConsumerFactory.Create<IFakeService>(() => new FakeServiceClient(), microsoftRetryPolicy.ForServiceConsumer()))
        //    //using (var service = ServiceConsumerFactory.Create<IFakeService>(() => new FakeServiceClient()))
        //    {
        //        //service.RetryPolicy = microsoftRetryPolicy.ForServiceConsumer();
        //        var result = await service.ConsumeAsync(operation => operation.AddTwoIntegersAsync(1, 1)).ConfigureAwait(false);

        //        if (result.HasNoException)
        //        {
        //            Console.WriteLine("Actual: " + result.Value);
        //            Assert.AreEqual(2, result.Value);
        //        }
        //        else
        //        {
        //            if (result.HasFaultException)
        //            {
        //                Console.WriteLine("Service operation threw a fault: " + result.Exception.ToString());
        //            }
        //            else if (result.HasException)
        //            {
        //                Console.WriteLine("Technical error occurred while calling the service operation: " + result.Exception.ToString());
        //            }

        //            Assert.Fail("Service operation was not successfully called");
        //        }
        //    }
        //}
    }
}
