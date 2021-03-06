﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FirstServiceLibrary
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Data", Namespace="http://schemas.datacontract.org/2004/07/FirstServiceLibrary")]
    public partial class Data : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string MessageField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message
        {
            get
            {
                return this.MessageField;
            }
            set
            {
                this.MessageField = value;
            }
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName="IFirstWCF")]
public interface IFirstWCF
{
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFirstWCF/MyFirstMethod", ReplyAction="http://tempuri.org/IFirstWCF/MyFirstMethodResponse")]
    string MyFirstMethod(string inputText);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFirstWCF/MyFirstMethod", ReplyAction="http://tempuri.org/IFirstWCF/MyFirstMethodResponse")]
    System.Threading.Tasks.Task<string> MyFirstMethodAsync(string inputText);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFirstWCF/MySecondMethod", ReplyAction="http://tempuri.org/IFirstWCF/MySecondMethodResponse")]
    FirstServiceLibrary.Data MySecondMethod(FirstServiceLibrary.Data inputText);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFirstWCF/MySecondMethod", ReplyAction="http://tempuri.org/IFirstWCF/MySecondMethodResponse")]
    System.Threading.Tasks.Task<FirstServiceLibrary.Data> MySecondMethodAsync(FirstServiceLibrary.Data inputText);
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IFirstWCFChannel : IFirstWCF, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class FirstWCFClient : System.ServiceModel.ClientBase<IFirstWCF>, IFirstWCF
{
    
    public FirstWCFClient()
    {
    }
    
    public FirstWCFClient(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public FirstWCFClient(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public FirstWCFClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public FirstWCFClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
    }
    
    public string MyFirstMethod(string inputText)
    {
        return base.Channel.MyFirstMethod(inputText);
    }
    
    public System.Threading.Tasks.Task<string> MyFirstMethodAsync(string inputText)
    {
        return base.Channel.MyFirstMethodAsync(inputText);
    }
    
    public FirstServiceLibrary.Data MySecondMethod(FirstServiceLibrary.Data inputText)
    {
        return base.Channel.MySecondMethod(inputText);
    }
    
    public System.Threading.Tasks.Task<FirstServiceLibrary.Data> MySecondMethodAsync(FirstServiceLibrary.Data inputText)
    {
        return base.Channel.MySecondMethodAsync(inputText);
    }
}
