using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Linq;

namespace Generator.ValueObject
{
    /// <summary>
    /// CCDA DTO Class
    /// </summary>
    [DataContract]
    [System.SerializableAttribute()]
    [XmlSerializerFormat]
    public class CDAObject : ModelBase
    {
        #region :  Key
        private string _TRST_ID;
        private int cdaObjectID;

        [DataMember]
        public string TRST_ID
        {
            get { return _TRST_ID; }
            set { _TRST_ID = value; OnPropertyChanged("TRST_ID"); }
        }


        public string GetTRST_ID() { return TRST_ID; }
        public void SetTRST_ID(string _TRST_ID) { TRST_ID = _TRST_ID; }
        public virtual int CDAObjectID
        {
            get { return cdaObjectID; }
            set { cdaObjectID = value; OnPropertyChanged("CDAObjectID"); }
        }
        public int GetCDAObjectID() { return CDAObjectID; }
        public void SetCDAObjectID(int _CDAObjectID) { CDAObjectID = _CDAObjectID; }

        #endregion

        #region :  Header
        private DocumentInformationObject documentInformation;
        private RecordTargetObject patient;
        private CustodianObject custodian;
        private AuthorObject author;
        private AuthenticatorObject authenticator;
        private InformationRecipientObject[] informationRecipient;
        private ConsentObject consent;
        private WithdrawalObject withdrawal;
        private GudianObject guardian;

        /// <summary>
        /// 서식 기본정보
        /// </summary>
        [DataMember]
        public virtual DocumentInformationObject DocumentInformation
        {
            get { return documentInformation; }
            set { if (documentInformation != value) { documentInformation = value; OnPropertyChanged("DocumentInformation"); } }
        }

        public DocumentInformationObject GetDocumentInformation() { return DocumentInformation; }
        public void SetDocumentInformation(DocumentInformationObject _DocumentInformation) { DocumentInformation = _DocumentInformation; }

        /// <summary>
        /// 환자 정보
        /// </summary>
        [DataMember]
        public virtual RecordTargetObject Patient
        {
            get { return patient; }
            set { if (patient != value) { patient = value; OnPropertyChanged("Patient"); } }
        }

        public RecordTargetObject GetPatient() { return Patient; }
        public void SetPatient(RecordTargetObject _Patient) { Patient = _Patient; }

        /// <summary>
        /// 의료기관 정보
        /// </summary>
        [DataMember]
        public virtual CustodianObject Custodian
        {
            get { return custodian; }
            set { if (custodian != value) { custodian = value; OnPropertyChanged("Custodian"); } }
        }

        public CustodianObject GetCustodian() { return Custodian; }
        public void SetCustodian(CustodianObject _Custodian) { Custodian = _Custodian; }

        /// <summary>
        /// 진료의 정보
        /// </summary>
        [DataMember]
        public virtual AuthorObject Author
        {
            get { return author; }
            set { if (author != value) { author = value; OnPropertyChanged("Author"); } }
        }

        public AuthorObject GetAuthor() { return Author; }
        public void SetAuthor(AuthorObject _Author) { Author = _Author; }

        /// <summary>
        /// 문서작성자
        /// </summary>
        [DataMember]
        public virtual AuthenticatorObject Authenticator
        {
            get { return authenticator; }
            set { if (authenticator != value) { authenticator = value; OnPropertyChanged("Authenticator"); } }
        }

        public AuthenticatorObject GetAuthenticator() { return Authenticator; }
        public void SetAuthenticator(AuthenticatorObject _Authenticator) { Authenticator = _Authenticator; }

        /// <summary>
        /// 수신기관 정보
        /// </summary>
        [DataMember]
        public virtual InformationRecipientObject[] InformationRecipient
        {
            get { return informationRecipient; }
            set { if (informationRecipient != value) { informationRecipient = value; OnPropertyChanged("InformationRecipient"); } }
        }
        public virtual List<InformationRecipientObject> InformationRecipientList
        {
            get { return InformationRecipient != null ? InformationRecipient.ToList() : null; }
            set { InformationRecipient = value != null ? value.ToArray() : null; OnPropertyChanged("InformationRecipient"); }
        }
        public InformationRecipientObject[] GetInformationRecipient() { return InformationRecipient; }
        public void SetInformationRecipient(InformationRecipientObject[] _InformationRecipient) { InformationRecipient = _InformationRecipient; }









        //public virtual List<InformationRecipientObject> InformationRecipientList
        //{
        //    get { return InformationRecipient != null ? InformationRecipient.ToList() : null; }
        //    set { InformationRecipient = value != null ? value.ToArray() : null; OnPropertyChanged("InformationRecipient"); }
        //}

        //public List<InformationRecipientObject> GetInformationRecipientList() { return InformationRecipientList; }
        //public void SetInformationRecipientList(List<InformationRecipientObject> _InformationRecipientList) { InformationRecipientList = _InformationRecipientList; }

        /// <summary>
        /// 동의정보
        /// </summary>
        [DataMember]
        public virtual ConsentObject Consent
        {
            get { return consent; }
            set { if (consent != value) { consent = value; OnPropertyChanged("Consent"); } }
        }

        public ConsentObject GetConsent() { return Consent; }
        public void SetConsent(ConsentObject _Consent) { Consent = _Consent; }

        /// <summary>
        /// 철회정보
        /// </summary>
        [DataMember]
        public virtual WithdrawalObject Withdrawal
        {
            get { return withdrawal; }
            set { if (withdrawal != value) { withdrawal = value; OnPropertyChanged("Withdrawal"); } }
        }

        public WithdrawalObject GetWithdrawal() { return Withdrawal; }
        public void SetWithdrawal(WithdrawalObject _Withdrawal) { Withdrawal = _Withdrawal; }

        /// <summary>
        /// 보호자 정보
        /// </summary>
        [DataMember]
        public virtual GudianObject Guardian
        {
            get { return guardian; }
            set { if (guardian != value) { guardian = value; OnPropertyChanged("Guardian"); } }
        }
        public GudianObject GetGuardian() { return Guardian; }
        public void SetGuardian(GudianObject _Guardian) { Guardian = _Guardian; }

        #endregion

        #region :  Body
        private ProblemObject[] problems;
        private MedicationObject[] medications;
        private LaboratoryTestObject[] laboratoryResults;
        private ProcedureObject[] procedures;

        private PlanOfTreatmentObject planOfTreatment;
        private AllergyObject[] allergies;
        private ImmunizationObject[] immunizations;
        private VitalSignsObject[] vitalSigns;
        private InfectionObject infection;
        private string reasonForReferral;
        private string reasonForTransfer;
        private string historyOfPastIllness;
        private AssessmentObject assessmentObject;
        //private ImageInterpretationObject imageInterpretation;
        private SocialHistoryObject socialHistory;
        private SignatureObject signature;

        private string specimenCount;
        private string pathologyCount;
        private string radiologyCount;
        private string functionalCount;
        private string careProgress;
        //private string acuityScale;
        private string labSummary;
        private string plannedCare;
        private TransferObject transfer;
        private DischargeMedicationObject[] dischargeMedications;
        private string preProcedure;
        private string postProcedure;
        private ImageReadingObject imageReading;
        //private HealthInsuranceObject healthInsurance;        

        /// <summary>
        /// 진단 정보
        /// </summary>
        [DataMember]
        public virtual ProblemObject[] Problems
        {
            get { return problems; }
            set { if (problems != value) { problems = value; OnPropertyChanged("Problems"); } }
        }
        public virtual List<ProblemObject> ProblemList
        {
            get { return Problems != null ? Problems.ToList() : null; }
            set { Problems = value != null ? value.ToArray() : null; OnPropertyChanged("Problems"); }
        }
        public ProblemObject[] GetProblems() { return Problems; }
        public void SetProblems(ProblemObject[] _Problems) { Problems = _Problems; }









        //public virtual List<ProblemObject> ProblemList
        //{
        //    get { return Problems != null ? Problems.ToList() : null; }
        //    set { Problems = value != null ? value.ToArray() : null; OnPropertyChanged("Problems"); }
        //}

        //public List<ProblemObject> GetProblemList() { return ProblemList; }
        //public void SetProblemList(List<ProblemObject> _ProblemList) { ProblemList = _ProblemList; }

        /// <summary>
        /// 투약 정보
        /// </summary>
        [DataMember]
        public virtual MedicationObject[] Medications
        {
            get { return medications; }
            set { if (medications != value) { medications = value; OnPropertyChanged("Medications"); } }
        }
        public virtual List<MedicationObject> MedicationList
        {
            get { return Medications != null ? Medications.ToList() : null; }
            set { Medications = value != null ? value.ToArray() : null; OnPropertyChanged("Medications"); }
        }
        public MedicationObject[] GetMedications() { return Medications; }
        public void SetMedications(MedicationObject[] _Medications) { Medications = _Medications; }









        //public virtual List<MedicationObject> MedicationList
        //{
        //    get { return Medications != null ? Medications.ToList() : null; }
        //    set { Medications = value != null ? value.ToArray() : null; OnPropertyChanged("Medications"); }
        //}

        //public List<MedicationObject> GetMedicationList() { return MedicationList; }
        //public void SetMedicationList(List<MedicationObject> _MedicationList) { MedicationList = _MedicationList; }

        /// <summary>
        /// 검사 정보
        /// </summary>
        [DataMember]
        public virtual LaboratoryTestObject[] LaboratoryTests
        {
            get { return laboratoryResults; }
            set { if (laboratoryResults != value) { laboratoryResults = value; OnPropertyChanged("LaboratoryTests"); } }
        }
        public virtual List<LaboratoryTestObject> LaboratoryTestList
        {
            get { return LaboratoryTests != null ? LaboratoryTests.ToList() : null; }
            set { LaboratoryTests = value != null ? value.ToArray() : null; OnPropertyChanged("LaboratoryTests"); }
        }
        public LaboratoryTestObject[] GetLaboratoryTests() { return LaboratoryTests; }
        public void SetLaboratoryTests(LaboratoryTestObject[] _LaboratoryTests) { LaboratoryTests = _LaboratoryTests; }









        //public virtual List<LaboratoryTestObject> LaboratoryTestList
        //{
        //    get { return LaboratoryTests != null ? LaboratoryTests.ToList() : null; }
        //    set { LaboratoryTests = value != null ? value.ToArray() : null; OnPropertyChanged("LaboratoryTests"); }
        //}

        //public List<LaboratoryTestObject> GetLaboratoryTestList() { return LaboratoryTestList; }
        //public void SetLaboratoryTestList(List<LaboratoryTestObject> _LaboratoryTestList) { LaboratoryTestList = _LaboratoryTestList; }

        /// <summary>
        /// 수술 정보
        /// </summary>
        [DataMember]
        public virtual ProcedureObject[] Procedures
        {
            get { return procedures; }
            set { if (procedures != value) { procedures = value; OnPropertyChanged("Procedures"); } }
        }
        public virtual List<ProcedureObject> ProcedureList
        {
            get { return Procedures != null ? Procedures.ToList() : null; }
            set { Procedures = value != null ? value.ToArray() : null; OnPropertyChanged("Procedures"); }
        }
        public ProcedureObject[] GetProcedures() { return Procedures; }
        public void SetProcedures(ProcedureObject[] _Procedures) { Procedures = _Procedures; }









        //public virtual List<ProcedureObject> ProcedureList
        //{
        //    get { return Procedures != null ? Procedures.ToList() : null; }
        //    set { Procedures = value != null ? value.ToArray() : null; OnPropertyChanged("Procedures"); }
        //}

        //public List<ProcedureObject> GetProcedureList() { return ProcedureList; }
        //public void SetProcedureList(List<ProcedureObject> _ProcedureList) { ProcedureList = _ProcedureList; }

        /// <summary>
        /// 알러지 정보
        /// </summary>
        [DataMember]
        public virtual AllergyObject[] Allergies
        {
            get { return allergies; }
            set { if (allergies != value) { allergies = value; OnPropertyChanged("Allergies"); } }
        }
        public virtual List<AllergyObject> AllergyList
        {
            get { return Allergies != null ? Allergies.ToList() : null; }
            set { Allergies = value != null ? value.ToArray() : null; OnPropertyChanged("Allergies"); }
        }
        public AllergyObject[] GetAllergies() { return Allergies; }
        public void SetAllergies(AllergyObject[] _Allergies) { Allergies = _Allergies; }









        //public virtual List<AllergyObject> AllergyList
        //{
        //    get { return Allergies != null ? Allergies.ToList() : null; }
        //    set { Allergies = value != null ? value.ToArray() : null; OnPropertyChanged("Allergies"); }
        //}

        //public List<AllergyObject> GetAllergyList() { return AllergyList; }
        //public void SetAllergyList(List<AllergyObject> _AllergyList) { AllergyList = _AllergyList; }

        /// <summary>
        /// 예약관련 정보
        /// </summary>
        [DataMember]
        public virtual PlanOfTreatmentObject PlanOfTreatment
        {
            get { return planOfTreatment; }
            set { if (planOfTreatment != value) { planOfTreatment = value; OnPropertyChanged("PlanOfTreatment"); } }
        }

        public PlanOfTreatmentObject GetPlanOfTreatment() { return PlanOfTreatment; }
        public void SetPlanOfTreatment(PlanOfTreatmentObject _PlanOfTreatment) { PlanOfTreatment = _PlanOfTreatment; }

        /// <summary>
        /// 예방접종내역
        /// </summary>
        [DataMember]
        public virtual ImmunizationObject[] Immunizations
        {
            get { return immunizations; }
            set { if (immunizations != value) { immunizations = value; OnPropertyChanged("Immunizations"); } }
        }
        public virtual List<ImmunizationObject> ImmunizationList
        {
            get { return Immunizations != null ? Immunizations.ToList() : null; }
            set { Immunizations = value != null ? value.ToArray() : null; OnPropertyChanged("Immunizations"); }
        }
        public ImmunizationObject[] GetImmunizations() { return Immunizations; }
        public void SetImmunizations(ImmunizationObject[] _Immunizations) { Immunizations = _Immunizations; }









        //public virtual List<ImmunizationObject> ImmunizationList
        //{
        //    get { return Immunizations != null ? Immunizations.ToList() : null; }
        //    set { Immunizations = value != null ? value.ToArray() : null; OnPropertyChanged("Immunizations"); }
        //}

        //public List<ImmunizationObject> GetImmunizationList() { return ImmunizationList; }
        //public void SetImmunizationList(List<ImmunizationObject> _ImmunizationList) { ImmunizationList = _ImmunizationList; }

        /// <summary>
        /// 생체신호 및 상태
        /// </summary>
        [DataMember]
        public virtual VitalSignsObject[] VitalSigns
        {
            get { return vitalSigns; }
            set { if (vitalSigns != value) { vitalSigns = value; OnPropertyChanged("VitalSigns"); } }
        }
        public virtual List<VitalSignsObject> VitalSignList
        {
            get { return VitalSigns != null ? VitalSigns.ToList() : null; }
            set { VitalSigns = value != null ? value.ToArray() : null; OnPropertyChanged("VitalSigns"); }
        }
        public VitalSignsObject[] GetVitalSigns() { return VitalSigns; }
        public void SetVitalSigns(VitalSignsObject[] _VitalSigns) { VitalSigns = _VitalSigns; }









        //public virtual List<VitalSignsObject> VitalSignList
        //{
        //    get { return VitalSigns != null ? VitalSigns.ToList() : null; }
        //    set { VitalSigns = value != null ? value.ToArray() : null; OnPropertyChanged("VitalSigns"); }
        //}

        //public List<VitalSignsObject> GetVitalSignList() { return VitalSignList; }
        //public void SetVitalSignList(List<VitalSignsObject> _VitalSignList) { VitalSignList = _VitalSignList; }

        /// <summary>
        /// 흡연상태/음주상태
        /// </summary>
        [DataMember]
        public virtual SocialHistoryObject SocialHistory
        {
            get { return socialHistory; }
            set { if (socialHistory != value) { socialHistory = value; OnPropertyChanged("SocialHistory"); } }
        }

        public SocialHistoryObject GetSocialHistory() { return SocialHistory; }
        public void SetSocialHistory(SocialHistoryObject _SocialHistory) { SocialHistory = _SocialHistory; }

        /// <summary>
        /// 법정 감염성 전염병
        /// </summary>
        [DataMember]
        public virtual InfectionObject Infection
        {
            get { return infection; }
            set { if (infection != value) { infection = value; OnPropertyChanged("Infection"); } }
        }

        public InfectionObject GetInfection() { return Infection; }
        public void SetInfection(InfectionObject _Infection) { Infection = _Infection; }

        /// <summary>
        /// 의뢰사유
        /// </summary>
        [DataMember]
        public virtual string ReasonForReferral
        {
            get { return reasonForReferral; }
            set { if (reasonForReferral != value) { reasonForReferral = value; OnPropertyChanged("ReasonForReferral"); } }
        }

        public string GetReasonForReferral() { return ReasonForReferral; }
        public void SetReasonForReferral(string _ReasonForReferral) { ReasonForReferral = _ReasonForReferral; }

        /// <summary>
        /// 회송사유
        /// </summary>
        [DataMember]
        public virtual string ReasonForTransfer
        {
            get { return reasonForTransfer; }
            set { if (reasonForTransfer != value) { reasonForTransfer = value; OnPropertyChanged("ReasonForTransfer"); } }
        }

        public string GetReasonForTransfer() { return ReasonForTransfer; }
        public void SetReasonForTransfer(string _ReasonForTransfer) { ReasonForTransfer = _ReasonForTransfer; }

        /// <summary>
        /// 과거병력
        /// </summary>
        [DataMember]
        public virtual string HistoryOfPastIllness
        {
            get { return historyOfPastIllness; }
            set { if (historyOfPastIllness != value) { historyOfPastIllness = value; OnPropertyChanged("HistoryOfPastIllness"); } }
        }

        public string GetHistoryOfPastIllness() { return HistoryOfPastIllness; }
        public void SetHistoryOfPastIllness(string _HistoryOfPastIllness) { HistoryOfPastIllness = _HistoryOfPastIllness; }

        /// <summary>
        /// 소견 및 주의사항
        /// </summary>
        [DataMember]
        public virtual AssessmentObject Assessment
        {
            get { return assessmentObject; }
            set { if (assessmentObject != value) { assessmentObject = value; OnPropertyChanged("Assessment"); } }
        }

        public AssessmentObject GetAssessment() { return Assessment; }
        public void SetAssessment(AssessmentObject _Assessment) { Assessment = _Assessment; }

        [DataMember]
        public virtual string SpecimenCount
        {
            get { return specimenCount; }
            set { if (specimenCount != value) { specimenCount = value; OnPropertyChanged("SpecimenCount"); } }
        }

        public string GetSpecimenCount() { return SpecimenCount; }
        public void SetSpecimenCount(string _SpecimenCount) { SpecimenCount = _SpecimenCount; }

        [DataMember]
        public virtual string PathologyCount
        {
            get { return pathologyCount; }
            set { if (pathologyCount != value) { pathologyCount = value; OnPropertyChanged("PathologyCount"); } }
        }

        public string GetPathologyCount() { return PathologyCount; }
        public void SetPathologyCount(string _PathologyCount) { PathologyCount = _PathologyCount; }

        [DataMember]
        public virtual string RadiologyCount
        {
            get { return radiologyCount; }
            set { if (radiologyCount != value) { radiologyCount = value; OnPropertyChanged("RadiologyCount"); } }
        }

        public string GetRadiologyCount() { return RadiologyCount; }
        public void SetRadiologyCount(string _RadiologyCount) { RadiologyCount = _RadiologyCount; }

        [DataMember]
        public virtual string FunctionalCount
        {
            get { return functionalCount; }
            set { if (functionalCount != value) { functionalCount = value; OnPropertyChanged("FunctionalCount"); } }
        }

        public string GetFunctionalCount() { return FunctionalCount; }
        public void SetFunctionalCount(string _FunctionalCount) { FunctionalCount = _FunctionalCount; }

        [DataMember]
        public virtual NonXMLBodyObject NonXMLBody { get; set; }

        public NonXMLBodyObject GetNonXMLBody() { return NonXMLBody; }
        public void SetNonXMLBody(NonXMLBodyObject _NonXMLBody) { NonXMLBody = _NonXMLBody; }

        /// <summary>
        /// 주요검사 결과
        /// </summary>
        [DataMember]
        public virtual string LabSummary
        {
            get { return labSummary; }
            set { if (labSummary != value) { labSummary = value; OnPropertyChanged("LabSummary"); } }
        }

        public string GetLabSummary() { return LabSummary; }
        public void SetLabSummary(string _LabSummary) { LabSummary = _LabSummary; }

        /// <summary>
        /// 향후 치료방침
        /// </summary>
        [DataMember]
        public virtual string PlannedCare
        {
            get { return plannedCare; }
            set { if (plannedCare != value) { plannedCare = value; OnPropertyChanged("PlannedCare"); } }
        }

        public string GetPlannedCare() { return PlannedCare; }
        public void SetPlannedCare(string _PlannedCare) { PlannedCare = _PlannedCare; }

        /// <summary>
        /// 치료(수술) 및 경과
        /// </summary>
        [DataMember]
        public virtual string CareProgress
        {
            get { return careProgress; }
            set { if (careProgress != value) { careProgress = value; OnPropertyChanged("CareProgress"); } }
        }

        public string GetCareProgress() { return CareProgress; }
        public void SetCareProgress(string _CareProgress) { CareProgress = _CareProgress; }

        /// <summary>
        /// 서명정보
        /// </summary>
        [DataMember]
        public virtual SignatureObject Signature
        {
            get { return signature; }
            set { signature = value; OnPropertyChanged("Signature"); }
        }

        public SignatureObject GetSignature() { return Signature; }
        public void SetSignature(SignatureObject _Signature) { Signature = _Signature; }

        /// <summary>
        /// 이송
        /// </summary>
        [DataMember]
        public virtual TransferObject Transfer
        {
            get { return transfer; }
            set { if (transfer != value) { transfer = value; OnPropertyChanged("Transfer"); } }
        }

        public TransferObject GetTransfer() { return Transfer; }
        public void SetTransfer(TransferObject _Transfer) { Transfer = _Transfer; }

        /// <summary>
        /// 퇴원투약 정보
        /// </summary>
        [DataMember]
        public virtual DischargeMedicationObject[] DischargeMedications
        {
            get { return dischargeMedications; }
            set { if (dischargeMedications != value) { dischargeMedications = value; OnPropertyChanged("DischargeMedications"); } }
        }
        public virtual List<DischargeMedicationObject> DischargeMedicationList
        {
            get { return DischargeMedications != null ? DischargeMedications.ToList() : null; }
            set { DischargeMedications = value != null ? value.ToArray() : null; OnPropertyChanged("DischargeMedications"); }
        }
        public DischargeMedicationObject[] GetDischargeMedications() { return DischargeMedications; }
        public void SetDischargeMedications(DischargeMedicationObject[] _DischargeMedications) { DischargeMedications = _DischargeMedications; }









        //public virtual List<DischargeMedicationObject> DischargeMedicationList
        //{
        //    get { return DischargeMedications != null ? DischargeMedications.ToList() : null; }
        //    set { DischargeMedications = value != null ? value.ToArray() : null; OnPropertyChanged("DischargeMedications"); }
        //}

        //public List<DischargeMedicationObject> GetDischargeMedicationList() { return DischargeMedicationList; }
        //public void SetDischargeMedicationList(List<DischargeMedicationObject> _DischargeMedicationList) { DischargeMedicationList = _DischargeMedicationList; }

        public virtual string PreProcedure
        {
            get { return "응급처치 전 환자상태"; }
            set { if (preProcedure != value) { preProcedure = value; OnPropertyChanged("PreProcedure"); } }
        }

        public string GetPreProcedure() { return PreProcedure; }
        public void SetPreProcedure(string _PreProcedure) { PreProcedure = _PreProcedure; }
        public virtual string PostProcedure
        {
            get { return "응급처치 후 환자상태"; }
            set { if (postProcedure != value) { postProcedure = value; OnPropertyChanged("PostProcedure"); } }
        }

        public string GetPostProcedure() { return PostProcedure; }
        public void SetPostProcedure(string _PostProcedure) { PostProcedure = _PostProcedure; }

        [DataMember]
        public virtual ImageReadingObject ImageReading
        {
            get { return imageReading; }
            set { if (imageReading != value) { imageReading = value; OnPropertyChanged("ImageReading"); } }
        }

        public ImageReadingObject GetImageReading() { return ImageReading; }
        public void SetImageReading(ImageReadingObject _ImageReading) { ImageReading = _ImageReading; }

        ///// <summary>
        ///// 건강 보험정보
        ///// </summary>
        //[DataMember]
        //public HealthInsuranceObject HealthInsurance
        //{
        //    get { return healthInsurance; }
        //    set { healthInsurance = value; OnPropertyChanged("HealthInsurance"); }
        //}
        #endregion
    }
}
