﻿//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

namespace xave.com.generator.cus.CodeModel {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CodeContainers", Namespace="http://schemas.datacontract.org/2004/07/xave.web.code.dto")]
    [System.SerializableAttribute()]
    public partial class CodeContainers : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private xave.com.generator.cus.CodeModel.Code[] CodeTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private xave.com.generator.cus.CodeModel.Format[] FormatTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private xave.com.generator.cus.CodeModel.KOSTOM_Diagnosis[] KostomField;
        
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
        public xave.com.generator.cus.CodeModel.Code[] CodeType {
            get {
                return this.CodeTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.CodeTypeField, value) != true)) {
                    this.CodeTypeField = value;
                    this.RaisePropertyChanged("CodeType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public xave.com.generator.cus.CodeModel.Format[] FormatType {
            get {
                return this.FormatTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.FormatTypeField, value) != true)) {
                    this.FormatTypeField = value;
                    this.RaisePropertyChanged("FormatType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public xave.com.generator.cus.CodeModel.KOSTOM_Diagnosis[] Kostom {
            get {
                return this.KostomField;
            }
            set {
                if ((object.ReferenceEquals(this.KostomField, value) != true)) {
                    this.KostomField = value;
                    this.RaisePropertyChanged("Kostom");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Code", Namespace="http://schemas.datacontract.org/2004/07/xave.web.code.dto")]
    [System.SerializableAttribute()]
    public partial class Code : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BindableVariableField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CodeCDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CodeClassificationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CodeIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CodeNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CodeTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ParentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UseYNField;
        
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
        public string BindableVariable {
            get {
                return this.BindableVariableField;
            }
            set {
                if ((object.ReferenceEquals(this.BindableVariableField, value) != true)) {
                    this.BindableVariableField = value;
                    this.RaisePropertyChanged("BindableVariable");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CodeCD {
            get {
                return this.CodeCDField;
            }
            set {
                if ((object.ReferenceEquals(this.CodeCDField, value) != true)) {
                    this.CodeCDField = value;
                    this.RaisePropertyChanged("CodeCD");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CodeClassification {
            get {
                return this.CodeClassificationField;
            }
            set {
                if ((object.ReferenceEquals(this.CodeClassificationField, value) != true)) {
                    this.CodeClassificationField = value;
                    this.RaisePropertyChanged("CodeClassification");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CodeID {
            get {
                return this.CodeIDField;
            }
            set {
                if ((this.CodeIDField.Equals(value) != true)) {
                    this.CodeIDField = value;
                    this.RaisePropertyChanged("CodeID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CodeName {
            get {
                return this.CodeNameField;
            }
            set {
                if ((object.ReferenceEquals(this.CodeNameField, value) != true)) {
                    this.CodeNameField = value;
                    this.RaisePropertyChanged("CodeName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CodeType {
            get {
                return this.CodeTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.CodeTypeField, value) != true)) {
                    this.CodeTypeField = value;
                    this.RaisePropertyChanged("CodeType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Parent {
            get {
                return this.ParentField;
            }
            set {
                if ((this.ParentField.Equals(value) != true)) {
                    this.ParentField = value;
                    this.RaisePropertyChanged("Parent");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UseYN {
            get {
                return this.UseYNField;
            }
            set {
                if ((object.ReferenceEquals(this.UseYNField, value) != true)) {
                    this.UseYNField = value;
                    this.RaisePropertyChanged("UseYN");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Format", Namespace="http://schemas.datacontract.org/2004/07/xave.web.code.dto")]
    [System.SerializableAttribute()]
    public partial class Format : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FormatCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int FormatIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FormatStyleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FormatTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UseYNField;
        
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
        public string FormatCode {
            get {
                return this.FormatCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.FormatCodeField, value) != true)) {
                    this.FormatCodeField = value;
                    this.RaisePropertyChanged("FormatCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int FormatID {
            get {
                return this.FormatIDField;
            }
            set {
                if ((this.FormatIDField.Equals(value) != true)) {
                    this.FormatIDField = value;
                    this.RaisePropertyChanged("FormatID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FormatStyle {
            get {
                return this.FormatStyleField;
            }
            set {
                if ((object.ReferenceEquals(this.FormatStyleField, value) != true)) {
                    this.FormatStyleField = value;
                    this.RaisePropertyChanged("FormatStyle");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FormatType {
            get {
                return this.FormatTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.FormatTypeField, value) != true)) {
                    this.FormatTypeField = value;
                    this.RaisePropertyChanged("FormatType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UseYN {
            get {
                return this.UseYNField;
            }
            set {
                if ((object.ReferenceEquals(this.UseYNField, value) != true)) {
                    this.UseYNField = value;
                    this.RaisePropertyChanged("UseYN");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="KOSTOM_Diagnosis", Namespace="http://schemas.datacontract.org/2004/07/xave.web.code.dto")]
    [System.SerializableAttribute()]
    public partial class KOSTOM_Diagnosis : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CCC_CDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CHN_NMField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CNPT_CDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CTGField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EDI_CDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ENG_NMField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ICD9CM_CDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ICNP_CDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string KCD_CDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string KOR_NMField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LOINC_CDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MED_USE_YNField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SZ_TPField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UMLSField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string VERField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string VOC_CDField;
        
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
        public string CCC_CD {
            get {
                return this.CCC_CDField;
            }
            set {
                if ((object.ReferenceEquals(this.CCC_CDField, value) != true)) {
                    this.CCC_CDField = value;
                    this.RaisePropertyChanged("CCC_CD");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CHN_NM {
            get {
                return this.CHN_NMField;
            }
            set {
                if ((object.ReferenceEquals(this.CHN_NMField, value) != true)) {
                    this.CHN_NMField = value;
                    this.RaisePropertyChanged("CHN_NM");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CNPT_CD {
            get {
                return this.CNPT_CDField;
            }
            set {
                if ((object.ReferenceEquals(this.CNPT_CDField, value) != true)) {
                    this.CNPT_CDField = value;
                    this.RaisePropertyChanged("CNPT_CD");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CTG {
            get {
                return this.CTGField;
            }
            set {
                if ((object.ReferenceEquals(this.CTGField, value) != true)) {
                    this.CTGField = value;
                    this.RaisePropertyChanged("CTG");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EDI_CD {
            get {
                return this.EDI_CDField;
            }
            set {
                if ((object.ReferenceEquals(this.EDI_CDField, value) != true)) {
                    this.EDI_CDField = value;
                    this.RaisePropertyChanged("EDI_CD");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ENG_NM {
            get {
                return this.ENG_NMField;
            }
            set {
                if ((object.ReferenceEquals(this.ENG_NMField, value) != true)) {
                    this.ENG_NMField = value;
                    this.RaisePropertyChanged("ENG_NM");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ICD9CM_CD {
            get {
                return this.ICD9CM_CDField;
            }
            set {
                if ((object.ReferenceEquals(this.ICD9CM_CDField, value) != true)) {
                    this.ICD9CM_CDField = value;
                    this.RaisePropertyChanged("ICD9CM_CD");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ICNP_CD {
            get {
                return this.ICNP_CDField;
            }
            set {
                if ((object.ReferenceEquals(this.ICNP_CDField, value) != true)) {
                    this.ICNP_CDField = value;
                    this.RaisePropertyChanged("ICNP_CD");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string KCD_CD {
            get {
                return this.KCD_CDField;
            }
            set {
                if ((object.ReferenceEquals(this.KCD_CDField, value) != true)) {
                    this.KCD_CDField = value;
                    this.RaisePropertyChanged("KCD_CD");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string KOR_NM {
            get {
                return this.KOR_NMField;
            }
            set {
                if ((object.ReferenceEquals(this.KOR_NMField, value) != true)) {
                    this.KOR_NMField = value;
                    this.RaisePropertyChanged("KOR_NM");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LOINC_CD {
            get {
                return this.LOINC_CDField;
            }
            set {
                if ((object.ReferenceEquals(this.LOINC_CDField, value) != true)) {
                    this.LOINC_CDField = value;
                    this.RaisePropertyChanged("LOINC_CD");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MED_USE_YN {
            get {
                return this.MED_USE_YNField;
            }
            set {
                if ((object.ReferenceEquals(this.MED_USE_YNField, value) != true)) {
                    this.MED_USE_YNField = value;
                    this.RaisePropertyChanged("MED_USE_YN");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SZ_TP {
            get {
                return this.SZ_TPField;
            }
            set {
                if ((object.ReferenceEquals(this.SZ_TPField, value) != true)) {
                    this.SZ_TPField = value;
                    this.RaisePropertyChanged("SZ_TP");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UMLS {
            get {
                return this.UMLSField;
            }
            set {
                if ((object.ReferenceEquals(this.UMLSField, value) != true)) {
                    this.UMLSField = value;
                    this.RaisePropertyChanged("UMLS");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string VER {
            get {
                return this.VERField;
            }
            set {
                if ((object.ReferenceEquals(this.VERField, value) != true)) {
                    this.VERField = value;
                    this.RaisePropertyChanged("VER");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string VOC_CD {
            get {
                return this.VOC_CDField;
            }
            set {
                if ((object.ReferenceEquals(this.VOC_CDField, value) != true)) {
                    this.VOC_CDField = value;
                    this.RaisePropertyChanged("VOC_CD");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CodeModel.ICodeModel")]
    public interface ICodeModel {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICodeModel/GetModel", ReplyAction="http://tempuri.org/ICodeModel/GetModelResponse")]
        void GetModel(xave.com.generator.cus.CodeModel.CodeContainers codeContainers);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICodeModel/GetModel", ReplyAction="http://tempuri.org/ICodeModel/GetModelResponse")]
        System.Threading.Tasks.Task GetModelAsync(xave.com.generator.cus.CodeModel.CodeContainers codeContainers);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICodeModelChannel : xave.com.generator.cus.CodeModel.ICodeModel, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CodeModelClient : System.ServiceModel.ClientBase<xave.com.generator.cus.CodeModel.ICodeModel>, xave.com.generator.cus.CodeModel.ICodeModel {
        
        public CodeModelClient() {
        }
        
        public CodeModelClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CodeModelClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CodeModelClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CodeModelClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void GetModel(xave.com.generator.cus.CodeModel.CodeContainers codeContainers) {
            base.Channel.GetModel(codeContainers);
        }
        
        public System.Threading.Tasks.Task GetModelAsync(xave.com.generator.cus.CodeModel.CodeContainers codeContainers) {
            return base.Channel.GetModelAsync(codeContainers);
        }
    }
}
