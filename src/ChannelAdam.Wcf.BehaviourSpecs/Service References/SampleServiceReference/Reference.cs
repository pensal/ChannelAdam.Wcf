﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChannelAdam.Wcf.BehaviourSpecs.SampleServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SampleServiceReference.ISampleService")]
    public interface ISampleService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISampleService/SampleOperation", ReplyAction="http://tempuri.org/ISampleService/SampleOperationResponse")]
        string SampleOperation(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISampleService/SampleOperation", ReplyAction="http://tempuri.org/ISampleService/SampleOperationResponse")]
        System.Threading.Tasks.Task<string> SampleOperationAsync(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISampleService/AddTwoIntegers", ReplyAction="http://tempuri.org/ISampleService/AddTwoIntegersResponse")]
        int AddTwoIntegers(int value1, int value2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISampleService/AddTwoIntegers", ReplyAction="http://tempuri.org/ISampleService/AddTwoIntegersResponse")]
        System.Threading.Tasks.Task<int> AddTwoIntegersAsync(int value1, int value2);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISampleServiceChannel : ChannelAdam.Wcf.BehaviourSpecs.SampleServiceReference.ISampleService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SampleServiceClient : System.ServiceModel.ClientBase<ChannelAdam.Wcf.BehaviourSpecs.SampleServiceReference.ISampleService>, ChannelAdam.Wcf.BehaviourSpecs.SampleServiceReference.ISampleService {
        
        public SampleServiceClient() {
        }
        
        public SampleServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SampleServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SampleServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SampleServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string SampleOperation(int value) {
            return base.Channel.SampleOperation(value);
        }
        
        public System.Threading.Tasks.Task<string> SampleOperationAsync(int value) {
            return base.Channel.SampleOperationAsync(value);
        }
        
        public int AddTwoIntegers(int value1, int value2) {
            return base.Channel.AddTwoIntegers(value1, value2);
        }
        
        public System.Threading.Tasks.Task<int> AddTwoIntegersAsync(int value1, int value2) {
            return base.Channel.AddTwoIntegersAsync(value1, value2);
        }
    }
}
