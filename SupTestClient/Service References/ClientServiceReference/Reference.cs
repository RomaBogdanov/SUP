﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SupTestClient.ClientServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CompositeType", Namespace="http://schemas.datacontract.org/2004/07/SupContract")]
    [System.SerializableAttribute()]
    public partial class CompositeType : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool BoolValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StringValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private SupTestClient.ClientServiceReference.TableName TableNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool BoolValue {
            get {
                return this.BoolValueField;
            }
            set {
                if ((this.BoolValueField.Equals(value) != true)) {
                    this.BoolValueField = value;
                    this.RaisePropertyChanged("BoolValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StringValue {
            get {
                return this.StringValueField;
            }
            set {
                if ((object.ReferenceEquals(this.StringValueField, value) != true)) {
                    this.StringValueField = value;
                    this.RaisePropertyChanged("StringValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SupTestClient.ClientServiceReference.TableName TableName {
            get {
                return this.TableNameField;
            }
            set {
                if ((this.TableNameField.Equals(value) != true)) {
                    this.TableNameField = value;
                    this.RaisePropertyChanged("TableName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TableName", Namespace="http://schemas.datacontract.org/2004/07/SupContract")]
    public enum TableName : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        TestTable1 = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        VisOrders = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        TestTable2Ado = 2,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ClientServiceReference.ITableService")]
    public interface ITableService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/GetData", ReplyAction="http://tempuri.org/ITableService/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/GetData", ReplyAction="http://tempuri.org/ITableService/GetDataResponse")]
        System.Threading.Tasks.Task<string> GetDataAsync(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/ITableService/GetDataUsingDataContractResponse")]
        SupTestClient.ClientServiceReference.CompositeType GetDataUsingDataContract(SupTestClient.ClientServiceReference.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/ITableService/GetDataUsingDataContractResponse")]
        System.Threading.Tasks.Task<SupTestClient.ClientServiceReference.CompositeType> GetDataUsingDataContractAsync(SupTestClient.ClientServiceReference.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/GetTable", ReplyAction="http://tempuri.org/ITableService/GetTableResponse")]
        System.Data.DataTable GetTable(SupTestClient.ClientServiceReference.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/GetTable", ReplyAction="http://tempuri.org/ITableService/GetTableResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> GetTableAsync(SupTestClient.ClientServiceReference.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/InsertRows", ReplyAction="http://tempuri.org/ITableService/InsertRowsResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(object[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SupTestClient.ClientServiceReference.CompositeType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SupTestClient.ClientServiceReference.TableName))]
        bool InsertRows(SupTestClient.ClientServiceReference.CompositeType composite, object[] objs);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/InsertRows", ReplyAction="http://tempuri.org/ITableService/InsertRowsResponse")]
        System.Threading.Tasks.Task<bool> InsertRowsAsync(SupTestClient.ClientServiceReference.CompositeType composite, object[] objs);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITableServiceChannel : SupTestClient.ClientServiceReference.ITableService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TableServiceClient : System.ServiceModel.ClientBase<SupTestClient.ClientServiceReference.ITableService>, SupTestClient.ClientServiceReference.ITableService {
        
        public TableServiceClient() {
        }
        
        public TableServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TableServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TableServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TableServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetData(int value) {
            return base.Channel.GetData(value);
        }
        
        public System.Threading.Tasks.Task<string> GetDataAsync(int value) {
            return base.Channel.GetDataAsync(value);
        }
        
        public SupTestClient.ClientServiceReference.CompositeType GetDataUsingDataContract(SupTestClient.ClientServiceReference.CompositeType composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
        
        public System.Threading.Tasks.Task<SupTestClient.ClientServiceReference.CompositeType> GetDataUsingDataContractAsync(SupTestClient.ClientServiceReference.CompositeType composite) {
            return base.Channel.GetDataUsingDataContractAsync(composite);
        }
        
        public System.Data.DataTable GetTable(SupTestClient.ClientServiceReference.CompositeType composite) {
            return base.Channel.GetTable(composite);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> GetTableAsync(SupTestClient.ClientServiceReference.CompositeType composite) {
            return base.Channel.GetTableAsync(composite);
        }
        
        public bool InsertRows(SupTestClient.ClientServiceReference.CompositeType composite, object[] objs) {
            return base.Channel.InsertRows(composite, objs);
        }
        
        public System.Threading.Tasks.Task<bool> InsertRowsAsync(SupTestClient.ClientServiceReference.CompositeType composite, object[] objs) {
            return base.Channel.InsertRowsAsync(composite, objs);
        }
    }
}
