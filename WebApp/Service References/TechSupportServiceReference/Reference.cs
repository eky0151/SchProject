﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApp.TechSupportServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TechSupportServiceReference.ITechSupportService1")]
    public interface ITechSupportService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITechSupportService1/UserLogin", ReplyAction="http://tempuri.org/ITechSupportService1/UserLoginResponse")]
        bool UserLogin(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITechSupportService1/UserLogin", ReplyAction="http://tempuri.org/ITechSupportService1/UserLoginResponse")]
        System.Threading.Tasks.Task<bool> UserLoginAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITechSupportService1/GetProfilePicture", ReplyAction="http://tempuri.org/ITechSupportService1/GetProfilePictureResponse")]
        string GetProfilePicture(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITechSupportService1/GetProfilePicture", ReplyAction="http://tempuri.org/ITechSupportService1/GetProfilePictureResponse")]
        System.Threading.Tasks.Task<string> GetProfilePictureAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITechSupportService1/GetUserProfilePicture", ReplyAction="http://tempuri.org/ITechSupportService1/GetUserProfilePictureResponse")]
        string GetUserProfilePicture(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITechSupportService1/GetUserProfilePicture", ReplyAction="http://tempuri.org/ITechSupportService1/GetUserProfilePictureResponse")]
        System.Threading.Tasks.Task<string> GetUserProfilePictureAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITechSupportService1/RegisterNewUser")]
        void RegisterNewUser(string fullName, string email, string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITechSupportService1/RegisterNewUser")]
        System.Threading.Tasks.Task RegisterNewUserAsync(string fullName, string email, string userName, string password);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITechSupportService1Channel : WebApp.TechSupportServiceReference.ITechSupportService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TechSupportService1Client : System.ServiceModel.ClientBase<WebApp.TechSupportServiceReference.ITechSupportService1>, WebApp.TechSupportServiceReference.ITechSupportService1 {
        
        public TechSupportService1Client() {
        }
        
        public TechSupportService1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TechSupportService1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TechSupportService1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TechSupportService1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool UserLogin(string username, string password) {
            return base.Channel.UserLogin(username, password);
        }
        
        public System.Threading.Tasks.Task<bool> UserLoginAsync(string username, string password) {
            return base.Channel.UserLoginAsync(username, password);
        }
        
        public string GetProfilePicture(string username) {
            return base.Channel.GetProfilePicture(username);
        }
        
        public System.Threading.Tasks.Task<string> GetProfilePictureAsync(string username) {
            return base.Channel.GetProfilePictureAsync(username);
        }
        
        public string GetUserProfilePicture(string username) {
            return base.Channel.GetUserProfilePicture(username);
        }
        
        public System.Threading.Tasks.Task<string> GetUserProfilePictureAsync(string username) {
            return base.Channel.GetUserProfilePictureAsync(username);
        }
        
        public void RegisterNewUser(string fullName, string email, string userName, string password) {
            base.Channel.RegisterNewUser(fullName, email, userName, password);
        }
        
        public System.Threading.Tasks.Task RegisterNewUserAsync(string fullName, string email, string userName, string password) {
            return base.Channel.RegisterNewUserAsync(fullName, email, userName, password);
        }
    }
}