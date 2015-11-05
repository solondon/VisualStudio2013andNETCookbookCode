using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace ServiceExtensibility
{
    class NonNegetiveOperationBehavior : Attribute, IOperationBehavior
    {
        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, System.ServiceModel.Dispatcher.ClientOperation clientOperation)
        {
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, System.ServiceModel.Dispatcher.DispatchOperation dispatchOperation)
        {
            var originalInvoker = dispatchOperation.Invoker;
            dispatchOperation.Invoker = new NoNegetiveInvoker(originalInvoker);
        }

        public void Validate(OperationDescription operationDescription)
        {
            if (operationDescription.Messages.Count < 2 || operationDescription.Messages[1].Body.ReturnValue.Type != typeof(double))
            {
                throw new InvalidOperationException("This behavior can only be applied on operation which returns double");
            }
        }
    }

    class BinaryEncoderContractBehavior : Attribute, IContractBehavior
    {
        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            if (endpoint.Binding.CreateBindingElements().Find<MessageEncodingBindingElement>() == null)
            {
                bindingParameters.Add(new BinaryMessageEncodingBindingElement());
            }
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
           
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.DispatchRuntime dispatchRuntime)
        {
           
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
           
        }
    }
    class LoggingEndpointBehavior : IEndpointBehavior
    {

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(new LoggingInspector());
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
           
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
    class RequireHttpsBehavior : Attribute, IServiceBehavior
    {
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (var endpoint in serviceDescription.Endpoints)
            {
                if (endpoint.Binding.Scheme != Uri.UriSchemeHttps)
                    throw new InvalidOperationException("This service requires HTTPS");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(MySecureTestService));
            //host.Description.Behaviors.Add(new RequireHttpsBehavior());
            host.Open();

            ChannelFactory<ISecureTestService> factory = new ChannelFactory<ISecureTestService>("SecureChannel");
            var serviceClient = factory.CreateChannel();
            serviceClient.MyDummyOperation("aa", "bbb");
            Console.WriteLine("Secure service is running");
            Console.ReadKey(true);
        }
    }
}
