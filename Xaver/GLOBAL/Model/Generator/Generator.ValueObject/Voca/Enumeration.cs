using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generator.ValueObject
{
    /// <summary>
    /// 기밀성 수준 구분
    /// </summary>
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute()]
    public enum Confidentiality
    {
        /// <summary>
        /// 보통 (기본값)
        /// </summary>
        [System.ComponentModel.Description("N")]
        Normal = 0,
        /// <summary>
        /// 높음
        /// </summary>
        [System.ComponentModel.Description("R")]
        High = 1,
        /// <summary>
        /// 매우 높음
        /// </summary>
        [System.ComponentModel.Description("V")]
        VeryHigh = 2
    }

    /// <summary>
    /// 진료유형 구분 
    /// </summary>
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute()]
    public enum CareTypes
    {
        [System.ComponentModel.Description("외래")]
        AMBULATORY = 0,
        [System.ComponentModel.Description("입원")]
        INPATIENT = 1,
        [System.ComponentModel.Description("응급")]
        EMERGENCY = 2,
        [System.ComponentModel.Description("선택되지 않음")]
        NONE = 3
    }


    /// <summary>
    /// 성벌 유형
    /// </summary>
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute()]
    public enum GenderType
    {
        [System.ComponentModel.Description("M")]
        Male = 0,
        [System.ComponentModel.Description("F")]
        Female = 1,
        [System.ComponentModel.Description("UN")]
        Undifferentiated = 2
    }

    /// <summary>
    /// 환자와의 관계유형
    /// </summary>
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute()]
    public enum RelationshipType
    {
        [System.ComponentModel.Description("본인")]
        Myself = 0,
        [System.ComponentModel.Description("가족")]
        Family = 1,
        [System.ComponentModel.Description("법정대리인")]
        LegalRepresentive = 2,
        [System.ComponentModel.Description("기타")]
        ETC = 3
    }

    /// <summary>
    /// 동의정책 유형
    /// </summary>
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute()]
    [System.ComponentModel.DefaultValue(PrivacyPolicyType.NONE)]
    public enum PrivacyPolicyType
    {
        //[System.ComponentModel.Description("개별동의")]  //삭제예정      
        //OPT_IN,        
        //[System.ComponentModel.Description("포괄동의")]  //삭제예정
        //OPT_OUT,        
        [System.ComponentModel.Description("동의(전체)")]
        ENTIRE_CONSENT = 0,
        [System.ComponentModel.Description("동의(부분)")]
        PARTIAL_CONSENT = 1,
        [System.ComponentModel.Description("동의철회(전체)")]
        ENTIRE_WITHDRAWAL = 2,
        [System.ComponentModel.Description("동의철회(부분)")]
        PARTIAL_WITHDRAWAL = 3,
        [System.ComponentModel.Description("선택되지 않음")]
        NONE = 4
    }

    /// <summary>
    /// 보호자 관계유형
    /// </summary>
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute()]
    //[System.ComponentModel.DefaultValue(GuardianType.Self)]
    public enum GuardianType
    {
        [System.ComponentModel.Description("FTH")]
        Father = 0,
        [System.ComponentModel.Description("MTH")]
        Mother = 1,
        [System.ComponentModel.Description("GRMTH")]
        GrandMother = 2,
        [System.ComponentModel.Description("GRFTH")]
        GrandFather = 3,
        [System.ComponentModel.Description("WIFE")]
        Wife = 4,
        [System.ComponentModel.Description("HUSB")]
        Husband = 5,
        [System.ComponentModel.Description("FAMMEMB")]
        Family = 6,
        [System.ComponentModel.Description("SONC")]
        Son = 7,
        [System.ComponentModel.Description("DAUC")]
        Daughther = 8,
        [System.ComponentModel.Description("GRNDDAU")]
        GrandDaughter = 9,
        [System.ComponentModel.Description("GRNDSON")]
        GrandSon = 10,
        [System.ComponentModel.Description("NBOR")]
        Neighbor = 11,
        [System.ComponentModel.Description("ROOM")]
        Roommate = 12,
        [System.ComponentModel.Description("ONESELF")]
        Self = 13
    }
}
