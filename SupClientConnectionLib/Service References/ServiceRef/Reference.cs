﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SupClientConnectionLib.ServiceRef {
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
        private SupClientConnectionLib.ServiceRef.TableName TableNameField;
        
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
        public SupClientConnectionLib.ServiceRef.TableName TableName {
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
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        VisOrderElements = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        VisVisitors = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        VisOrganizations = 5,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceRef.ITableService", CallbackContract=typeof(SupClientConnectionLib.ServiceRef.ITableServiceCallback))]
    public interface ITableService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/GetData", ReplyAction="http://tempuri.org/ITableService/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/GetData", ReplyAction="http://tempuri.org/ITableService/GetDataResponse")]
        System.Threading.Tasks.Task<string> GetDataAsync(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/ITableService/GetDataUsingDataContractResponse")]
        SupClientConnectionLib.ServiceRef.CompositeType GetDataUsingDataContract(SupClientConnectionLib.ServiceRef.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/ITableService/GetDataUsingDataContractResponse")]
        System.Threading.Tasks.Task<SupClientConnectionLib.ServiceRef.CompositeType> GetDataUsingDataContractAsync(SupClientConnectionLib.ServiceRef.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/GetTable", ReplyAction="http://tempuri.org/ITableService/GetTableResponse")]
        System.Data.DataTable GetTable(SupClientConnectionLib.ServiceRef.CompositeType composite, string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/GetTable", ReplyAction="http://tempuri.org/ITableService/GetTableResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> GetTableAsync(SupClientConnectionLib.ServiceRef.CompositeType composite, string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/InsertRow", ReplyAction="http://tempuri.org/ITableService/InsertRowResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SupClientConnectionLib.ServiceRef.CompositeType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SupClientConnectionLib.ServiceRef.TableName))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(object[]))]
        bool InsertRow(SupClientConnectionLib.ServiceRef.CompositeType composite, object[] objs, string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/InsertRow", ReplyAction="http://tempuri.org/ITableService/InsertRowResponse")]
        System.Threading.Tasks.Task<bool> InsertRowAsync(SupClientConnectionLib.ServiceRef.CompositeType composite, object[] objs, string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/UpdateRow", ReplyAction="http://tempuri.org/ITableService/UpdateRowResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SupClientConnectionLib.ServiceRef.CompositeType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SupClientConnectionLib.ServiceRef.TableName))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(object[]))]
        bool UpdateRow(SupClientConnectionLib.ServiceRef.CompositeType composite, int rowNumber, object[] objs, string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/UpdateRow", ReplyAction="http://tempuri.org/ITableService/UpdateRowResponse")]
        System.Threading.Tasks.Task<bool> UpdateRowAsync(SupClientConnectionLib.ServiceRef.CompositeType composite, int rowNumber, object[] objs, string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/DeleteRow", ReplyAction="http://tempuri.org/ITableService/DeleteRowResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SupClientConnectionLib.ServiceRef.CompositeType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SupClientConnectionLib.ServiceRef.TableName))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(object[]))]
        bool DeleteRow(SupClientConnectionLib.ServiceRef.CompositeType composite, object[] row, string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/DeleteRow", ReplyAction="http://tempuri.org/ITableService/DeleteRowResponse")]
        System.Threading.Tasks.Task<bool> DeleteRowAsync(SupClientConnectionLib.ServiceRef.CompositeType composite, object[] row, string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/GetImage", ReplyAction="http://tempuri.org/ITableService/GetImageResponse")]
        byte[] GetImage(int id, string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/GetImage", ReplyAction="http://tempuri.org/ITableService/GetImageResponse")]
        System.Threading.Tasks.Task<byte[]> GetImageAsync(int id, string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/Authorize", ReplyAction="http://tempuri.org/ITableService/AuthorizeResponse")]
        bool Authorize(string login, string pass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/Authorize", ReplyAction="http://tempuri.org/ITableService/AuthorizeResponse")]
        System.Threading.Tasks.Task<bool> AuthorizeAsync(string login, string pass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/CheckAuthorize", ReplyAction="http://tempuri.org/ITableService/CheckAuthorizeResponse")]
        bool CheckAuthorize(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/CheckAuthorize", ReplyAction="http://tempuri.org/ITableService/CheckAuthorizeResponse")]
        System.Threading.Tasks.Task<bool> CheckAuthorizeAsync(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/ExitAuthorize", ReplyAction="http://tempuri.org/ITableService/ExitAuthorizeResponse")]
        bool ExitAuthorize(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITableService/ExitAuthorize", ReplyAction="http://tempuri.org/ITableService/ExitAuthorizeResponse")]
        System.Threading.Tasks.Task<bool> ExitAuthorizeAsync(string login);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITableServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITableService/InsRow")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SupClientConnectionLib.ServiceRef.CompositeType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SupClientConnectionLib.ServiceRef.TableName))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(object[]))]
        void InsRow(string tableName, object[] objs);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITableService/UpdRow")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SupClientConnectionLib.ServiceRef.CompositeType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SupClientConnectionLib.ServiceRef.TableName))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(object[]))]
        void UpdRow(string tableName, int rowNumber, object[] objs);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ITableService/DelRow")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SupClientConnectionLib.ServiceRef.CompositeType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SupClientConnectionLib.ServiceRef.TableName))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(object[]))]
        void DelRow(string tableName, object[] objs);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITableServiceChannel : SupClientConnectionLib.ServiceRef.ITableService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TableServiceClient : System.ServiceModel.DuplexClientBase<SupClientConnectionLib.ServiceRef.ITableService>, SupClientConnectionLib.ServiceRef.ITableService {
        
        public TableServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public TableServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public TableServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public TableServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public TableServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public string GetData(int value) {
            return base.Channel.GetData(value);
        }
        
        public System.Threading.Tasks.Task<string> GetDataAsync(int value) {
            return base.Channel.GetDataAsync(value);
        }
        
        public SupClientConnectionLib.ServiceRef.CompositeType GetDataUsingDataContract(SupClientConnectionLib.ServiceRef.CompositeType composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
        
        public System.Threading.Tasks.Task<SupClientConnectionLib.ServiceRef.CompositeType> GetDataUsingDataContractAsync(SupClientConnectionLib.ServiceRef.CompositeType composite) {
            return base.Channel.GetDataUsingDataContractAsync(composite);
        }
        
        public System.Data.DataTable GetTable(SupClientConnectionLib.ServiceRef.CompositeType composite, string login) {
            return base.Channel.GetTable(composite, login);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> GetTableAsync(SupClientConnectionLib.ServiceRef.CompositeType composite, string login) {
            return base.Channel.GetTableAsync(composite, login);
        }
        
        public bool InsertRow(SupClientConnectionLib.ServiceRef.CompositeType composite, object[] objs, string login) {
            return base.Channel.InsertRow(composite, objs, login);
        }
        
        public System.Threading.Tasks.Task<bool> InsertRowAsync(SupClientConnectionLib.ServiceRef.CompositeType composite, object[] objs, string login) {
            return base.Channel.InsertRowAsync(composite, objs, login);
        }
        
        public bool UpdateRow(SupClientConnectionLib.ServiceRef.CompositeType composite, int rowNumber, object[] objs, string login) {
            return base.Channel.UpdateRow(composite, rowNumber, objs, login);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateRowAsync(SupClientConnectionLib.ServiceRef.CompositeType composite, int rowNumber, object[] objs, string login) {
            return base.Channel.UpdateRowAsync(composite, rowNumber, objs, login);
        }
        
        public bool DeleteRow(SupClientConnectionLib.ServiceRef.CompositeType composite, object[] row, string login) {
            return base.Channel.DeleteRow(composite, row, login);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteRowAsync(SupClientConnectionLib.ServiceRef.CompositeType composite, object[] row, string login) {
            return base.Channel.DeleteRowAsync(composite, row, login);
        }
        
        public byte[] GetImage(int id, string login) {
            return base.Channel.GetImage(id, login);
        }
        
        public System.Threading.Tasks.Task<byte[]> GetImageAsync(int id, string login) {
            return base.Channel.GetImageAsync(id, login);
        }
        
        public bool Authorize(string login, string pass) {
            return base.Channel.Authorize(login, pass);
        }
        
        public System.Threading.Tasks.Task<bool> AuthorizeAsync(string login, string pass) {
            return base.Channel.AuthorizeAsync(login, pass);
        }
        
        public bool CheckAuthorize(string login) {
            return base.Channel.CheckAuthorize(login);
        }
        
        public System.Threading.Tasks.Task<bool> CheckAuthorizeAsync(string login) {
            return base.Channel.CheckAuthorizeAsync(login);
        }
        
        public bool ExitAuthorize(string login) {
            return base.Channel.ExitAuthorize(login);
        }
        
        public System.Threading.Tasks.Task<bool> ExitAuthorizeAsync(string login) {
            return base.Channel.ExitAuthorizeAsync(login);
        }
    }
}
