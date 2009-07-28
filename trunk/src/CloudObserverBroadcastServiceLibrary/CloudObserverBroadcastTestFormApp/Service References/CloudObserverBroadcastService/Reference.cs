﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3074
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CloudObserverBroadcastTestFormApp.CloudObserverBroadcastService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CloudObserverBroadcastService.ICloudObserverBroadcastService")]
    public interface ICloudObserverBroadcastService {
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.EncryptAndSign, Action="http://tempuri.org/ICloudObserverBroadcastService/bindMeToCamera", ReplyAction="http://tempuri.org/ICloudObserverBroadcastService/bindMeToCameraResponse")]
        void bindMeToCamera(int clientID, int cameraID, int ident);
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.EncryptAndSign, Action="http://tempuri.org/ICloudObserverBroadcastService/setMeAsCamera", ReplyAction="http://tempuri.org/ICloudObserverBroadcastService/setMeAsCameraResponse")]
        void setMeAsCamera(int cameraID, int ident);
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.EncryptAndSign, Action="http://tempuri.org/ICloudObserverBroadcastService/getNextFrame", ReplyAction="http://tempuri.org/ICloudObserverBroadcastService/getNextFrameResponse")]
        byte[] getNextFrame(int cameraID, int ident);
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.EncryptAndSign, Action="http://tempuri.org/ICloudObserverBroadcastService/setNextFrame", ReplyAction="http://tempuri.org/ICloudObserverBroadcastService/setNextFrameResponse")]
        void setNextFrame(byte[] frame, int ident);
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.EncryptAndSign, Action="http://tempuri.org/ICloudObserverBroadcastService/clean", ReplyAction="http://tempuri.org/ICloudObserverBroadcastService/cleanResponse")]
        void clean();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface ICloudObserverBroadcastServiceChannel : CloudObserverBroadcastTestFormApp.CloudObserverBroadcastService.ICloudObserverBroadcastService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class CloudObserverBroadcastServiceClient : System.ServiceModel.ClientBase<CloudObserverBroadcastTestFormApp.CloudObserverBroadcastService.ICloudObserverBroadcastService>, CloudObserverBroadcastTestFormApp.CloudObserverBroadcastService.ICloudObserverBroadcastService {
        
        public CloudObserverBroadcastServiceClient() {
        }
        
        public CloudObserverBroadcastServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CloudObserverBroadcastServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CloudObserverBroadcastServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CloudObserverBroadcastServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void bindMeToCamera(int clientID, int cameraID, int ident) {
            base.Channel.bindMeToCamera(clientID, cameraID, ident);
        }
        
        public void setMeAsCamera(int cameraID, int ident) {
            base.Channel.setMeAsCamera(cameraID, ident);
        }
        
        public byte[] getNextFrame(int cameraID, int ident) {
            return base.Channel.getNextFrame(cameraID, ident);
        }
        
        public void setNextFrame(byte[] frame, int ident) {
            base.Channel.setNextFrame(frame, ident);
        }
        
        public void clean() {
            base.Channel.clean();
        }
    }
}
