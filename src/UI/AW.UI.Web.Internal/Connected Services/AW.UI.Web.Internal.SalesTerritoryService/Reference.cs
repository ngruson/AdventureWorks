﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AW.UI.Web.Internal.SalesTerritoryService
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://services.aw.com/SalesTerritoryService/1.0", ConfigurationName="AW.UI.Web.Internal.SalesTerritoryService.ISalesTerritoryService")]
    public interface ISalesTerritoryService
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="ListTerritories", ReplyAction="ListTerritories")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<AW.UI.Web.Internal.SalesTerritoryService.ListTerritoriesResponse> ListTerritoriesAsync(AW.UI.Web.Internal.SalesTerritoryService.ListTerritoriesRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://services.aw.com/SalesTerritoryService/1.0/ListTerritories")]
    public partial class TerritoryDto
    {
        
        private string nameField;
        
        private string countryRegionCodeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string CountryRegionCode
        {
            get
            {
                return this.countryRegionCodeField;
            }
            set
            {
                this.countryRegionCodeField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ListTerritories", WrapperNamespace="http://services.aw.com/SalesTerritoryService/1.0", IsWrapped=true)]
    public partial class ListTerritoriesRequest
    {
        
        public ListTerritoriesRequest()
        {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ListTerritoriesResponse", WrapperNamespace="http://services.aw.com/SalesTerritoryService/1.0", IsWrapped=true)]
    public partial class ListTerritoriesResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://services.aw.com/SalesTerritoryService/1.0", Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Territories", Namespace="http://services.aw.com/SalesTerritoryService/1.0/ListTerritories", IsNullable=false)]
        public AW.UI.Web.Internal.SalesTerritoryService.TerritoryDto[] ListTerritoriesResult;
        
        public ListTerritoriesResponse()
        {
        }
        
        public ListTerritoriesResponse(AW.UI.Web.Internal.SalesTerritoryService.TerritoryDto[] ListTerritoriesResult)
        {
            this.ListTerritoriesResult = ListTerritoriesResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public interface ISalesTerritoryServiceChannel : AW.UI.Web.Internal.SalesTerritoryService.ISalesTerritoryService, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public partial class SalesTerritoryServiceClient : System.ServiceModel.ClientBase<AW.UI.Web.Internal.SalesTerritoryService.ISalesTerritoryService>, AW.UI.Web.Internal.SalesTerritoryService.ISalesTerritoryService
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public SalesTerritoryServiceClient() : 
                base(SalesTerritoryServiceClient.GetDefaultBinding(), SalesTerritoryServiceClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_ISalesTerritoryService.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SalesTerritoryServiceClient(EndpointConfiguration endpointConfiguration) : 
                base(SalesTerritoryServiceClient.GetBindingForEndpoint(endpointConfiguration), SalesTerritoryServiceClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SalesTerritoryServiceClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(SalesTerritoryServiceClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SalesTerritoryServiceClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(SalesTerritoryServiceClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SalesTerritoryServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<AW.UI.Web.Internal.SalesTerritoryService.ListTerritoriesResponse> AW.UI.Web.Internal.SalesTerritoryService.ISalesTerritoryService.ListTerritoriesAsync(AW.UI.Web.Internal.SalesTerritoryService.ListTerritoriesRequest request)
        {
            return base.Channel.ListTerritoriesAsync(request);
        }
        
        public System.Threading.Tasks.Task<AW.UI.Web.Internal.SalesTerritoryService.ListTerritoriesResponse> ListTerritoriesAsync()
        {
            AW.UI.Web.Internal.SalesTerritoryService.ListTerritoriesRequest inValue = new AW.UI.Web.Internal.SalesTerritoryService.ListTerritoriesRequest();
            return ((AW.UI.Web.Internal.SalesTerritoryService.ISalesTerritoryService)(this)).ListTerritoriesAsync(inValue);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_ISalesTerritoryService))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_ISalesTerritoryService))
            {
                return new System.ServiceModel.EndpointAddress("http://localhost/AW.SalesTerritoryService/SalesTerritoryService.svc");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return SalesTerritoryServiceClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_ISalesTerritoryService);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return SalesTerritoryServiceClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_ISalesTerritoryService);
        }
        
        public enum EndpointConfiguration
        {
            
            BasicHttpBinding_ISalesTerritoryService,
        }
    }
}