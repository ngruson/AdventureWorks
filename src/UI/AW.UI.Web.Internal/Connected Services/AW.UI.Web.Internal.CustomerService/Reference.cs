﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AW.UI.Web.Internal.CustomerService
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://services.aw.com/CustomerService/1.0", ConfigurationName="AW.UI.Web.Internal.CustomerService.ICustomerService")]
    public interface ICustomerService
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="ListCustomers", ReplyAction="ListCustomers")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<AW.UI.Web.Internal.CustomerService.ListCustomersResponseListCustomersResult> ListCustomersAsync(AW.UI.Web.Internal.CustomerService.ListCustomersRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://services.aw.com/CustomerService/1.0")]
    public partial class ListCustomersRequest
    {
        
        private int pageIndexField;
        
        private int pageSizeField;
        
        private string territoryField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public int PageIndex
        {
            get
            {
                return this.pageIndexField;
            }
            set
            {
                this.pageIndexField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public int PageSize
        {
            get
            {
                return this.pageSizeField;
            }
            set
            {
                this.pageSizeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string Territory
        {
            get
            {
                return this.territoryField;
            }
            set
            {
                this.territoryField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://services.aw.com/CustomerService/1.0/ListCustomers")]
    public partial class StoreDto
    {
        
        private string nameField;
        
        private string salesPersonField;
        
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
        public string SalesPerson
        {
            get
            {
                return this.salesPersonField;
            }
            set
            {
                this.salesPersonField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://services.aw.com/CustomerService/1.0/ListCustomers")]
    public partial class PersonDto
    {
        
        private string titleField;
        
        private string firstNameField;
        
        private string middleNameField;
        
        private string lastNameField;
        
        private string fullNameField;
        
        private string suffixField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string Title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string FirstName
        {
            get
            {
                return this.firstNameField;
            }
            set
            {
                this.firstNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string MiddleName
        {
            get
            {
                return this.middleNameField;
            }
            set
            {
                this.middleNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string LastName
        {
            get
            {
                return this.lastNameField;
            }
            set
            {
                this.lastNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string FullName
        {
            get
            {
                return this.fullNameField;
            }
            set
            {
                this.fullNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string Suffix
        {
            get
            {
                return this.suffixField;
            }
            set
            {
                this.suffixField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://services.aw.com/CustomerService/1.0/ListCustomers")]
    public partial class CustomerDto
    {
        
        private string accountNumberField;
        
        private string salesTerritoryNameField;
        
        private PersonDto personField;
        
        private StoreDto storeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string AccountNumber
        {
            get
            {
                return this.accountNumberField;
            }
            set
            {
                this.accountNumberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string SalesTerritoryName
        {
            get
            {
                return this.salesTerritoryNameField;
            }
            set
            {
                this.salesTerritoryNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public PersonDto Person
        {
            get
            {
                return this.personField;
            }
            set
            {
                this.personField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public StoreDto Store
        {
            get
            {
                return this.storeField;
            }
            set
            {
                this.storeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://services.aw.com/CustomerService/1.0")]
    public partial class ListCustomersResponseListCustomersResult
    {
        
        private CustomerDto[] customersField;
        
        private int totalCustomersField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Customers", Namespace="http://services.aw.com/CustomerService/1.0/ListCustomers", Order=0)]
        public CustomerDto[] Customers
        {
            get
            {
                return this.customersField;
            }
            set
            {
                this.customersField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public int TotalCustomers
        {
            get
            {
                return this.totalCustomersField;
            }
            set
            {
                this.totalCustomersField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public interface ICustomerServiceChannel : AW.UI.Web.Internal.CustomerService.ICustomerService, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public partial class CustomerServiceClient : System.ServiceModel.ClientBase<AW.UI.Web.Internal.CustomerService.ICustomerService>, AW.UI.Web.Internal.CustomerService.ICustomerService
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public CustomerServiceClient() : 
                base(CustomerServiceClient.GetDefaultBinding(), CustomerServiceClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_ICustomerService.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public CustomerServiceClient(EndpointConfiguration endpointConfiguration) : 
                base(CustomerServiceClient.GetBindingForEndpoint(endpointConfiguration), CustomerServiceClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public CustomerServiceClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(CustomerServiceClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public CustomerServiceClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(CustomerServiceClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public CustomerServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<AW.UI.Web.Internal.CustomerService.ListCustomersResponseListCustomersResult> ListCustomersAsync(AW.UI.Web.Internal.CustomerService.ListCustomersRequest request)
        {
            return base.Channel.ListCustomersAsync(request);
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
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_ICustomerService))
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
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_ICustomerService))
            {
                return new System.ServiceModel.EndpointAddress("http://localhost/AW.CustomerService/CustomerService.svc");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return CustomerServiceClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_ICustomerService);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return CustomerServiceClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_ICustomerService);
        }
        
        public enum EndpointConfiguration
        {
            
            BasicHttpBinding_ICustomerService,
        }
    }
}
